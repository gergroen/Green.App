Green.App.PreparePage(new StartPage());
function StartPage() {
    var self = this;
    self.PageId = "#startpage";
    self.Pageinit = function() {
    };
    self.ViewModel = {
        StatusIntervalId: "",
        Status: ko.observable(),
        PageShow: function (params) {
            self.ViewModel.CheckOnlineStatus();
            self.ViewModel.StatusIntervalId = setInterval(self.ViewModel.CheckOnlineStatus, 2000);
        },
        CheckOnlineStatus: function () {
            $.ajax({
                cache: false,
                timeout: 1000,
                url: "http://gerard-laptop/api/isonline",
                dataType: 'json',
                success: self.ViewModel.OnStatusOnline,
                error: self.ViewModel.OnStatusOffline
            });
        },
        OnStatusOnline: function () {
            self.ViewModel.Status(self.ViewModel.Resources.online);
        },
        OnStatusOffline: function () {
            self.ViewModel.Status(self.ViewModel.Resources.offline);
        },
        PageHide: function () {
            clearInterval(self.ViewModel.StatusIntervalId);
        },
        Resources: {},
        ResourceDictionary: {
            en : {
                title: "Start",
                online: "Online",
                offline: "Offline"
            }
        }
    };
};