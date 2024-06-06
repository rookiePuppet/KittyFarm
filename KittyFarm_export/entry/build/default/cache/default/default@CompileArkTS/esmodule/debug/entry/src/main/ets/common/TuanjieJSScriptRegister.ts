import { RegisterOHData } from "@bundle:com.PuppetStudio.KittyFarm/entry/ets/OHData";
import { RegisterTestClass } from "@bundle:com.PuppetStudio.KittyFarm/entry/ets/TestClass";
function register(tuanjieJSClasses, functionName) {
    var exportObj = functionName();
    for (let key of Object.keys(exportObj)) {
        tuanjieJSClasses[key] = exportObj[key];
    }
}
export function registerJSScriptToCSharp() {
    var tuanjieJSClasses = {};
    register(tuanjieJSClasses, RegisterOHData);
    register(tuanjieJSClasses, RegisterTestClass);
    return tuanjieJSClasses;
}
