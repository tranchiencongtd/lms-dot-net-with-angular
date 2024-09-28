import { Course } from "./course";

export class PurchaseDetailsModel {
    idCourse : number;
    idStudent?: number;
    priceOfCourse: number;
    priceOfDiscount: number;
    idPurchaseOrder?: number;
    courses?: Course;
}