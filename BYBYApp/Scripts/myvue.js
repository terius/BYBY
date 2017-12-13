Vue.component('select2', {
    props: ['options', 'value', 'id'],
    template: '<select class="form-control select2me" :id="id" :name="id"  ><option></option></select>',
    mounted: function () {
        var vm = this
        $(this.$el)
            // init select2
            .select2({
                data: this.options,
                placeholder: "Select",
                width: "resolve",
                allowClear: true,
                language: "zh-CN"
            })
            .val(this.value)
            .trigger('change')
        // emit event on change.
        //.on('change', function () {
        //    vm.$emit('input', this.value)
        //})

    },
    watch: {
        value: function (value) {
            // update value
            //   alert(value);
            $(this.$el).val(value).trigger('change')
        },
        options: function (options) {
            // update options
            $(this.$el).empty().select2({ data: options })
        }
    },
    destroyed: function () {
        $(this.$el).off().select2('destroy')
    }
})


Vue.component('edit-input', {
    props: ['id', 'text', 'myclass', 'value', 'maxlength'],
    template: '<div class="form-group">'
    + '<label :for="id" > {{text }}<slot name="othertext"></slot></label > '
    + '<input type="text" :value="value" :maxlength="maxlength" @input="updateSelf($event.target.value)" class="form-control" :class="myclass" :id="id" :name="id" ></div > ',
    methods: {
        updateSelf(value) {
            this.$emit('input', value)
        }
    }
})


Vue.component('date-picker', {
    props: ['id', 'text', 'myclass', 'value', 'defaultDate'],
    template: '<div class="form-group">'
    + '<label :for="id" > {{text }}<slot name="othertext"></slot></label > '
    + '<input type="text" :value="value"   readonly @input="updateSelf($event.target.value)" class="form-control date-picker" :class="myclass" :id="id" :name="id" ></div > ',
    mounted: function () {
        var vm = this
        var option = {
            autoclose: true,
            format: "yyyy-mm-dd",
            language: "zh-CN",
            clearBtn: true
        }
        var dfdate = this.$attrs.defaultdate;
        if (dfdate) {
            var sp = dfdate.split('-');
            option.defaultViewDate = { year: sp[0], month: sp[1] - 1, day: 1 }
        }
        $(".date-picker", this.$el).datepicker(option).on('change', function () {
            vm.$emit('input', this.value)
        });

    },
    methods: {
        updateSelf(value) {
            this.$emit('input', value)
        }
    }
})

Vue.component('date-picker-simple', {
    props: ['id', 'myclass', 'value', 'defaultDate'],
    template: '<input type="text" :value="value"   readonly @input="updateSelf($event.target.value)" class="form-control date-picker" :class="myclass" :id="id" :name="id" >',
    mounted: function () {
        var vm = this
        var option = {
            autoclose: true,
            format: "yyyy-mm-dd",
            language: "zh-CN",
            clearBtn: true
        }
        var dfdate = this.$attrs.defaultdate;
        if (dfdate) {
            var sp = dfdate.split('-');
            option.defaultViewDate = { year: sp[0], month: sp[1] - 1, day: 1 }
        }
        $(this.$el).datepicker(option).on('change', function () {
            vm.$emit('input', this.value)
        });

    },
    methods: {
        updateSelf(value) {
            this.$emit('input', value)
        }
    }
})


//测试
Vue.component('test-vue', {
    props: ['sid', 'eid', 'myclass', 'value', 'defaultDate'],
    template: '<div>'
    + '<input type="text" :id="sid" class="form-control s-time" :value="value[0]" :class="myclass" :name="sid" @input="value[0] = $event.target.value"> '
    + '<input type="text" :id="eid" class="form-control e-time" :value="value[1]" :class="myclass" :name="eid" @input="value[1] = $event.target.value"> '
    + '</div>',
    watch: {
        value: function (value) {
            // update value
               alert(value);
            $(".s-time", this.$el).val(value[0])
            $(".e-time", this.$el).val(value[1])
        }
    },

})



