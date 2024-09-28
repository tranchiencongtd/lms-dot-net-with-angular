import { Injectable } from "@angular/core";
import { Stripe, loadStripe } from "@stripe/stripe-js";
import { LinkSettings } from "src/app/library/linksetting/LinkSetting";
import { PurchaseOrder } from "src/app/models/models/purchase";
import { StripeCheckoutSession } from "src/app/models/respone_model/stripe-checkout-session";
import { environment } from "src/environments/environment";
import { TransferHttp } from "../transfer-http/transfer-http";
import { map } from "rxjs/operators";
import { RepositoryModel } from "src/app/models/models/repository_base";

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
}