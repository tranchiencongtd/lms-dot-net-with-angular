import { String } from '../share-function/string';

export class LinkSettings {
    public static GetResLinkSetting(pGroup: string, pFunction: string, ...pParams: any[]) {
        const resLinkSetting: any = require('../../../assets/document/reslink-api.json');
        const link = resLinkSetting[pGroup][pFunction];
        return String.Format(link, pParams);
    }
}
