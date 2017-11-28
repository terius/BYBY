Vue.component('select2', {
    props: ['options', 'value', 'sattr'],
    template: '<select class="form-control select2me" v-bind:class="sattr.class" '
    + ' v-bind:data-placeholder="sattr.placeholder"  ></select>',
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
    +'<input type="text" :value="value" @input="updateSelf($event.target.value)" class="form-control" :class="myclass" :id="id" :name="id" ></div > ',
    methods: {
        updateSelf(value) {
            this.$emit('input', value)
        }
    }
})

