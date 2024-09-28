import { Injectable } from "@angular/core";
import { TransferHttp } from "../transfer-http/transfer-http";
import { SystemConfig } from "src/app/models/models/system-config";
import { LinkSettings } from "src/app/library/linksetting/LinkSetting";
import { RepositoryModel } from "src/app/models/models/repository_base";
import { map } from "rxjs/operators";

@Injectable({ providedIn: 'root' })
export class SystemConfigServices {
    constructor(
        private transferHttp: TransferHttp
    ) { }

    updateConfigSystem(model: SystemConfig) {
        const ApiUrl = LinkSettings.GetResLinkSetting('MasterData', 'UpdateInformationConfigSystem', model.id);
        return this.transferHttp.put(ApiUrl, model).pipe(map((res: RepositoryModel<boolean>) => res));
    }

    getSystemConfig() {
        const ApiUrl = LinkSettings.GetResLinkSetting('MasterData', 'GetListConfigSystem');
        return this.transferHttp.get(ApiUrl).pipe(map((res: RepositoryModel<SystemConfig[]>) => res));
    }
}