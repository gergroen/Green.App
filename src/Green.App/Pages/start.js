Green.App.PreparePage(new StartPage());
function StartPage() {
    var self = this;
    self.PageId = "#startpage";
    self.Pageinit = function() {
    };
    self.ViewModel = {
        PageShow: function(params) {
        },
        PageHide: function() {
        },
        Resources: {},
        ResourceDictionary: {
            en : {
                title: "Start"
            }
        }
    };
};