var com = {}


com.valtrim = function (val) {
    if (val != null && val != undefined && typeof val == "string") {
        return $.trim(val.replace(/<[^>]*>/g, ""));
    }
    return val;

}

com.jqFormOption = {
    data: {},
    error: function (result, textStatus, errorThrown) {
        alert('查询出错');
    },
    beforeSerialize: function ($form, options) {
        $form.find(":text,select,:hidden").each(function (index, ele) {
            ele.value = com.valtrim(ele.value);
        })

    }
}
