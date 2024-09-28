import { Course } from "../models/course";
import { Partner } from "../models/partner";

export class HomeDataModel {
    partners?: Partner[];
    listContentData: ListContentData[];
}


export class ListContentData {
    nameContent?: string;
    listDataOfContent?: Course[];
}