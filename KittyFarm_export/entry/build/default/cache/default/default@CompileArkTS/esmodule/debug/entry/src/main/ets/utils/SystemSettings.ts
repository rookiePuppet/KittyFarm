import settings from "@ohos:settings";
import { GetFromGlobalThis } from "@bundle:com.PuppetStudio.KittyFarm/entry/ets/common/GlobalThisUtil";
import type { Context } from "@ohos:abilityAccessCtrl";
import { TuanjieLog } from "@bundle:com.PuppetStudio.KittyFarm/entry/ets/common/TuanjieLog";
export class SystemSettings {
    static isAutoRotationLocked(): boolean {
        let context: Context = GetFromGlobalThis('AbilityContext');
        if (context == null || context == undefined) {
            TuanjieLog.warn('can not get context from globalThis');
            return true;
        }
        return settings.getValueSync(context, settings.general.ACCELEROMETER_ROTATION_STATUS, '0') === '0';
    }
}
