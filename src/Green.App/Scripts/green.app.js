var Green = Green || {};
Green.App = (function() {
    var self = this;
    self.CurrentLanguage = "en";
    self.PreparePage = function(page) {
        $(document).delegate(page.PageId, 'pagebeforecreate', function() {
            if (page.ViewModel && page.ViewModel.ResourceDictionary && page.ViewModel.Resources) {
                page.ViewModel.Resources = page.ViewModel.ResourceDictionary[Green.App.CurrentLanguage];
                ko.applyBindings(page.ViewModel, $(page.PageId)[0]);
            }
        });
        $(document).delegate(page.PageId, 'pageinit', function() {
            if (page.PageInit) {
                page.PageInit();
            }
        });
        $(document).delegate(page.PageId, 'pageshow', function() {
            if (page.ViewModel && page.ViewModel.PageShow) {
                var params = Green.Utils.GetURLParameters();
                page.ViewModel.PageShow(params);
            }
        });
        $(document).delegate(page.PageId, 'pagehide', function() {
            if (page.ViewModel && page.ViewModel.PageHide) {
                page.ViewModel.PageHide();
            }
        });
    };
    return self;
})();
Green.Utils = (function () {
    var self = this;
    self.GetURLParameter = function(name) {
        var data;
        if (location.hash) {
            data = location.hash;
        } else {
            data = location.href;
        }
        return decodeURIComponent(decodeURI(
            (RegExp(name + '=' + '(.+?)(&|$)').exec(data) || [, null])[1]
        ));
    };
    self.GetURLParameters = function() {
        var params = {};
        var data;
        if (location.hash) {
            data = location.hash;
        } else {
            data = location.href;
        }
        var queryIndex = data.indexOf("?");
        if (queryIndex > -1) {
            var queryStr = data.substr(queryIndex + 1, data.length - queryIndex - 1);
            var values = queryStr.split('&');
            $.each(values, function(index, item) {
                var param = item.split('=');
                if (param.length == 2) {
                    params[param[0]] = param[1];
                }
            });
        }
        return params;
    };
    return self;
})();