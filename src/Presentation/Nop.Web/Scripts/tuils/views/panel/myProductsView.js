define(['jquery', 'underscore', 'baseView', 'util'],
    function ($, _, BaseView, TuilsUtil) {
            
        var MyProductsView = BaseView.extend({
            events: {
                'keyup .search-pdt .searchinput': 'search'
            },
            initialize: function(args){
            
            },
            search: function (obj) {
                if (obj.keyCode != 13) {
                    var value = $(obj.target).val();
                    //this.$('.btn_search').attr("href", "?q=" + value);
                    this.$('.btn_search').attr("href", TuilsUtil.updateQueryStringParameter(document.location.href, 'q', value));
                }
                else {
                    document.location.href = this.$('.search-pdt a').attr("href");
                }
            },
        });

        return MyProductsView; 
});