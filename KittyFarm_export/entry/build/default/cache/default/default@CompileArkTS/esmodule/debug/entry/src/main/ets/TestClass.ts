export class ClassObjectTest {
    intFunc(a, b) {
        return a + b;
    }
}
export function RegisterTestClass() {
    var register = {};
    register["ClassObjectTest"] = new ClassObjectTest();
    return register;
}
