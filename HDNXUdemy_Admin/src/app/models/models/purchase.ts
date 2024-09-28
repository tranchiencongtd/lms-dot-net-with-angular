import { ValuePurchaseOrderCount } from "../respone_model/value-of-data-count";
import { InformationManualBankingModel } from "./infomation-banking";
import { PurchaseDetailsModel } from "./purchase-details";
import { StudentUser } from "./student-user";

export class PurchaseOrder {
    id?: number;
    idStudent?: number;
    contentTranferBanking?: string;
    totalPrice?: number;
    discountAmount?: number;
    purcharseStatus?: number;
    purcharseCode?: string;
    listPurchaseCourseDetails?: PurchaseDetailsModel[];
    infoBanking?: InformationManualBankingModel;
    purchaseDate?: Date;
    isCheck?: boolean;
    createDate?: Date;
    status?: string;
    user?: StudentUser;
    nameStatus: string;
    valueOfDataCount: ValuePurchaseOrderCount[];
}