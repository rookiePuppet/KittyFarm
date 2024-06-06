
import { RegisterOHData } from '../OHData';
import { RegisterTestClass } from '../TestClass';

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