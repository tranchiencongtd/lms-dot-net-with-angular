import { Injectable } from "@angular/core";
import { TransferHttp } from "../transfer-http/transfer-http";
import { LinkSettings } from "src/app/library/linksetting/LinkSetting";
import { map } from "rxjs/operators";
import { PageResult, RepositoryModel } from "src/app/models/models/repository_base";
import { PurchaseOrder } from "src/app/models/models/purchase";
import { InformationManualBankingModel } from "src/app/models/models/infomation-banking";

@Injectable({ providedIn: 'root' })
export class PurchaseServices {
    constructor(
        private transferHttp: TransferHttp
    ) { }

    genPurchaseOrder(idStudent: number) {
        const ApiUrl = LinkSettings.GetResLinkSetting('PurchaseOrder', 'GenPurchaseOrder', idStudent);
        return this.transferHttp.get(ApiUrl).pipe(map((res: RepositoryModel<string>) => res));

    }

    createRequestPurchase(model: PurchaseOrder) {
        const ApiUrl = LinkSettings.GetResLinkSetting('PurchaseOrder', 'CreateRequestPurchase');
        return this.transferHttp.post(ApiUrl, model).pipe(map((res: RepositoryModel<PurchaseOrder>) => res));
    }

    getInformationOfBanking() {
        const ApiUrl = LinkSettings.GetResLinkSetting('MasterData', 'GetListInformationManualBanking');
        return this.transferHttp.get(ApiUrl).pipe(map((res: RepositoryModel<InformationManualBankingModel>) => res));
    }

    isCheckPurchaseCourse(idCourse: number) {
        const ApiUrl = LinkSettings.GetResLinkSetting('PurchaseOrder', 'IsCheckCoursePurchase', idCourse);
        return this.transferHttp.get(ApiUrl).pipe(map((res: RepositoryModel<boolean>) => res));

    }

    getListPurchaseOrder(pageIndex: number, pageSize: number) {
        const ApiUrl = LinkSettings.GetResLinkSetting('PurchaseOrder', 'GetListPurcharseCourses', pageIndex, pageSize);
        return this.transferHttp.get(ApiUrl).pipe(map((res: RepositoryModel<PageResult<PurchaseOrder[]>>) => res));
    }

    updateStatusPurchase(model: PurchaseOrder) {
        const ApiUrl = LinkSettings.GetResLinkSetting('PurchaseOrder', 'UpdateStatusPurchase', model.id);
        return this.transferHttp.put(ApiUrl, model).pipe(map((res: RepositoryModel<PageResult<PurchaseOrder[]>>) => res));
    }

    getPurchaseCourseDetail(idPurchase: number) {
        const ApiUrl = LinkSettings.GetResLinkSetting('PurchaseOrder', 'GetPurchaseCorseDetail', idPurchase);
        return this.transferHttp.get(ApiUrl).pipe(map((res: RepositoryModel<PurchaseOrder>) => res));
    }

}