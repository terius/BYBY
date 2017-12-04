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
    },
    updated: function () {
        alert(value);
    }
})


Vue.component('edit-input', {
    props: ['id', 'text', 'myclass', 'value','maxlength'],
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



Vue.component('ttt', {
    props: ['sid', 'eid', 'myclass', 'svalue', 'evalue', 'defaultDate'],
    template: '<div>'
    + '<input type="text" :id="sid" class="form-control" :value="svalue" :class="myclass" :name="sid" > '
    + '<input type="text" :id="eid" class="form-control" :value="evalue" :class="myclass" :name="eid" > '
    + '</div>'
})

Vue.component('date-range-picker', {
    props: ['sid', 'eid', 'myclass', 'svalue', 'evalue', 'defaultDate'],
    template: '<div class="input-group date-picker input-daterange">'
    +'<input type="text" :id="sid" class="form-control" :value="svalue" :class="myclass" :name="sid" @input="updateSelf($event.target.value)"> '
    +'<span class="input-group-addon" > 到 </span>'
    +'<input type="text" :id="eid" class="form-control" :value="evalue" :class="myclass" :name="eid" @input="updateSelf2($event.target.value)"> '
    +'</div>',
    mounted: function () {
        var vm = this
        var option = {
            language: "zh-CN",
            autoclose: true,
            format: "yyyy-mm-dd",
            clearBtn: true,
            todayHighlight: true
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
        },
        updateSelf2(value) {
            this.$emit('input', value)
        }
    }
})

