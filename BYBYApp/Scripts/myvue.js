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
            .on('change', function () {
                vm.$emit('input', this.value)
            })
        //if (this.value != null) {
        //    $(this.$el).val(this.value).trigger('change')
        //}
    },
    watch: {
        value: function (value) {
            // update value
            $(this.$el).val(value)
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
    props: ['id', 'text', 'myclass', 'value'],
    template: '<div class="form-group">'
    + '<label :for="id" > {{text }}<slot name="othertext"></slot></label > '
    + '<input type="text" :value="value" @input="updateSelf($event.target.value)" class="form-control" :class="myclass" :id="id" :name="id" ></div > ',
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

