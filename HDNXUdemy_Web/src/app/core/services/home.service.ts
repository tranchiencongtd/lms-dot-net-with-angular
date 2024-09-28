import { Injectable } from "@angular/core";
import { TransferHttp } from "../transfer-http/transfer-http";
import { LinkSettings } from "src/app/library/linksetting/LinkSetting";
import { map } from "rxjs/operators";
import { RepositoryModel } from "src/app/models/models/repository_base";
import { HomeDataModel } from "src/app/models/respone_model/home-data";

@Injectable({ providedIn: 'root' })
export class HomeServices {
    constructor(
        private transferHttp: TransferHttp
    ) { }

    getHomeData() {
        const ApiUrl = LinkSettings.GetResLinkSetting('Home', 'GetDataForHome');
        return this.transferHttp.get(ApiUrl).pipe(map((res: RepositoryModel<HomeDataModel>) => res));

    }
}