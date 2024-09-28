import { Injectable } from "@angular/core";
import { TransferHttp } from "../transfer-http/transfer-http";
import { StudentUser } from "src/app/models/models/student-user";
import { LinkSettings } from "src/app/library/linksetting/LinkSetting";
import { map } from "rxjs/operators";
import { RepositoryModel } from "src/app/models/models/repository_base";

@Injectable({ providedIn: 'root' })
export class StudentServices {
    constructor(
        private transferHttp: TransferHttp
    ) { }

    createStudent(model: StudentUser) {
        const ApiUrl = LinkSettings.GetResLinkSetting('Student', 'CreateStudent');
        return this.transferHttp.post(ApiUrl, model).pipe(map((res: RepositoryModel<boolean>) => res));
    }

    updateStatusStudent(model: StudentUser) {
        const ApiUrl = LinkSettings.GetResLinkSetting('Student', 'UpdateStatusStudent', model.id);
        return this.transferHttp.put(ApiUrl, model).pipe(map((res: RepositoryModel<boolean>) => res));
    }

    updateStudent(model: StudentUser) {
        const ApiUrl = LinkSettings.GetResLinkSetting('Student', 'UpdateStudent', model.id);
        return this.transferHttp.put(ApiUrl, model).pipe(map((res: RepositoryModel<boolean>) => res));
    }

    getStudents() {
        const ApiUrl = LinkSettings.GetResLinkSetting('Student', 'GetListStudents');
        return this.transferHttp.get(ApiUrl).pipe(map((res: RepositoryModel<StudentUser[]>) => res));
    }

    getStudent(id: number) {
        const ApiUrl = LinkSettings.GetResLinkSetting('Student', 'GetStudent', id);
        return this.transferHttp.get(ApiUrl).pipe(map((res: RepositoryModel<StudentUser>) => res));
    }

    getUserOfProject() {
        const ApiUrl = LinkSettings.GetResLinkSetting('Student', 'GetListUserManager');
        return this.transferHttp.get(ApiUrl).pipe(map((res: RepositoryModel<StudentUser[]>) => res));
    }
}