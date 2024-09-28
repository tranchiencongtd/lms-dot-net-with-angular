import { Injectable } from "@angular/core";
import { TransferHttp } from "../transfer-http/transfer-http";
import { LinkSettings } from "src/app/library/linksetting/LinkSetting";
import { map } from "rxjs/operators";
import { RepositoryModel } from "src/app/models/models/repository_base";
import { FileTypeUpload } from "src/app/models/respone_model/file-type-upload";

@Injectable({ providedIn: 'root' })
export class UploadFileToServerServices {
    constructor(
        private transferHttp: TransferHttp
    ) { }

    getFileTypeUpload() {
        const ApiUrl = LinkSettings.GetResLinkSetting('MasterData', 'ListFileTypeUpload');
        return this.transferHttp.get(ApiUrl).pipe(map((res: RepositoryModel<FileTypeUpload[]>) => res));
    }

    getFileOfTypeUpload() {
        const ApiUrl = LinkSettings.GetResLinkSetting('MasterData', 'GetListOfFileTypeUpload');
        return this.transferHttp.get(ApiUrl).pipe(map((res: RepositoryModel<FileTypeUpload[]>) => res));
    }

}