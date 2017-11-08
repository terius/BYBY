var com = {}


com.valtrim = function (val) {
    if (val != null && val != undefined && typeof val == "string") {
        return $.trim(val.replace(/<[^>]*>/g, ""));
    }
    return val;

}
