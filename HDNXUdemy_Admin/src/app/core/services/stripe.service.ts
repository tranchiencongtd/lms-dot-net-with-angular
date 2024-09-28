import { Injectable } from "@angular/core";
import { Stripe, loadStripe } from "@stripe/stripe-js";
import { LinkSettings } from "src/app/library/linksetting/LinkSetting";
import { PurchaseOrder } from "src/app/models/models/purchase";
import { StripeCheckoutSession } from "src/app/models/respone_model/stripe-checkout-session";
import { environment } from "src/environments/environment";
import { TransferHttp } from "../transfer-http/transfer-http";
import { map } from "rxjs/operators";
import { PageResult, RepositoryModel } from "src/app/models/models/repository_base";
import { CouponModel } from "src/app/models/models/coupon";
import { CouponPromotionModel } from "src/app/models/request_model/coupon-promotion";
import { PromotionCodeModel } from "src/app/models/models/promotion-code";

@Injectable({ providedIn: 'root' })
export class StripeServices {
    stripePromise: Promise<Stripe>

    constructor(
        private transferHttp: TransferHttp
    ) {
        this.loadConfigForStripe();
    }

    private loadConfigForStripe() {
        this.stripePromise = loadStripe(environment.stripePublicKey);
    }

    async goToCheckOutForStripe(model: PurchaseOrder): Promise<void> {
        const stripe = await this.stripePromise;
        this.createCheckoutSession(model).subscribe(async (res) => {
            if (res.systemMessage === '' && res.retCode === 0) {
                await stripe.redirectToCheckout({
                    sessionId: res.data.sessionId,
                });
            }
        });

    }

    private createCheckoutSession(model: PurchaseOrder) {
        const ApiUrl = LinkSettings.GetResLinkSetting('PurchaseOrder', 'StripeCreateCheckoutSession');
        return this.transferHttp.post(ApiUrl, model).pipe(map((res: RepositoryModel<StripeCheckoutSession>) => res));
    }

    getListCouponAndPromotion(pageIndex: number, pageSize: number) {
        const ApiUrl = LinkSettings.GetResLinkSetting('CouponPromotion', 'GetListCouponActiveOnSystem', pageIndex, pageSize);
        return this.transferHttp.get(ApiUrl).pipe(map((res: RepositoryModel<PageResult<CouponModel[]>>) => res));
    }

    createCouponPromotionCode(model: CouponPromotionModel) {
        const ApiUrl = LinkSettings.GetResLinkSetting('CouponPromotion', 'CreatePromotionCoupon');
        return this.transferHttp.post(ApiUrl, model).pipe(map((res: RepositoryModel<boolean>) => res));
    }

    updateCouponPromotionCode(model: CouponPromotionModel) {
        const ApiUrl = LinkSettings.GetResLinkSetting('CouponPromotion', 'UpdatePromotionCoupon');
        return this.transferHttp.put(ApiUrl, model).pipe(map((res: RepositoryModel<boolean>) => res));
    }

    deleteCouponForPromotion(stripeCouponId: string) {
        const ApiUrl = LinkSettings.GetResLinkSetting('CouponPromotion', 'DeleteCouponForPromotion', stripeCouponId);
        return this.transferHttp.putUrl(ApiUrl).pipe(map((res: RepositoryModel<boolean>) => res));
    }

    activeCouponPromotion(id: number) {
        const ApiUrl = LinkSettings.GetResLinkSetting('CouponPromotion', 'ActiveCouponPromotion', id);
        return this.transferHttp.putUrl(ApiUrl).pipe(map((res: RepositoryModel<boolean>) => res));
    }

    getListPromotions(pageIndex: number, pageSize: number) {
        const ApiUrl = LinkSettings.GetResLinkSetting('CouponPromotion', 'GetListPromotions', pageIndex, pageSize);
        return this.transferHttp.get(ApiUrl).pipe(map((res: RepositoryModel<PageResult<PromotionCodeModel[]>>) => res));
    }
}