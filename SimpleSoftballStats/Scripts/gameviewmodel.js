var ObjectState = {
    Unchanged: 0,
    Added: 1,
    Modified: 2,
    Deleted: 3
}

var playerMapping = {
    'AvailablePlayers': {
        key: function (player) {
            return ko.utils.unwrapObservable(player.PlayerId);
        },
        create: function (options) {
            return new PlayerViewModel(options.data);
        }
    }
}

PlayerViewModel = function (data) {
    var self = this;
    ko.mapping.fromJS(data, playerMapping, self);
}


var gameBoxScoreDetailMapping = {
    'BoxScoreDetails': {
        key: function (boxScoreDetail) {
            return ko.utils.unwrapObservable(boxScoreDetail.PlayerId);
        },
        create: function (options) {
            return new GameBoxScoreDetailViewModel(options.data);
        }
    }
};


var dataConverter = function (key, value) {
    if (key === 'RowVersion' && Array.isArray(value)) {
        var str = String.fromCharCode.apply(null, value);
        return btoa(str);
    }

    return value;
};


GameBoxScoreDetailViewModel = function (data) {
    var self = this;
    ko.mapping.fromJS(data, gameBoxScoreDetailMapping, self);

    self.flagBoxScoreDetailAsEdited = function () {
        if (self.ObjectState() != ObjectState.Added) {
            self.ObjectState(ObjectState.Modified);
        }

        return true;
    }, 

    self.PlayerFullName = ko.computed(function () {
        if (self.Player != null)
            return self.Player.FirstName() + " " + self.Player.LastName();
        else
            return "";
    });
};


GameViewModel = function (data) {
    var self = this;
    ko.mapping.fromJS(data, gameBoxScoreDetailMapping, self);

    self.save = function () {
        $.ajax({
            url: "/Games/Save/",
            type: "POST",
            data: ko.toJSON(self),
            contentType: "application/json",
            success: function (data) {
                if (data.gameViewModel != null)
                    ko.mapping.fromJS(data.gameViewModel, gameBoxScoreDetailMapping, self);

                if (data.newLocation != null)
                    window.location = data.newLocation;
            }
        });
    },

    self.flagGameAsEdited = function () {

        if (self.ObjectState() != ObjectState.Added) {
            self.ObjectState(ObjectState.Modified);
        }

        return true;
    }, 

    self.addGameBoxScoreDetail = function () {
        var gameBoxScoreDetail = new GameBoxScoreDetailViewModel
            ({
                GameId: self.Id(), PlayerId: 1, BattingOrder: 0, PlateAppearances: 1,
                RunsScored: 0, Hits: 0, Doubles: 0, Triples: 0, HomeRuns: 0, RunsBattedIn: 0, Walks: 0, 
                ObjectState: ObjectState.Added
            });
        //gameBoxScoreDetail.Player = new PlayerViewModel({ PlayerId: 1, FirstName: "Jim", LastName: "C.", FullName: "Jim C." });
        self.BoxScoreDetails.push(gameBoxScoreDetail);
    },

    self.deleteBoxScoreDetail = function (boxScoreDetail) {
        self.BoxScoreDetails.remove(this);
        if (self.BoxScoreDetailsToDelete.indexOf(boxScoreDetail.PlayerId()) == -1) {
            self.BoxScoreDetailsToDelete.push(boxScoreDetail.PlayerId);
        }
    }, 

    self.TotalPA = ko.computed(function() {
        var total = 0;
        ko.utils.arrayForEach(self.BoxScoreDetails(), function(boxScoreDetail) {
            total += parseFloat(boxScoreDetail.PlateAppearances());
        });
        return total.toFixed(0);
    }), 

    self.TotalRuns = ko.computed(function() {
        var total = 0;
        ko.utils.arrayForEach(self.BoxScoreDetails(), function(boxScoreDetail) {
            total += parseFloat(boxScoreDetail.RunsScored());
        });
        return total.toFixed(0);
    }), 

    self.TotalHits = ko.computed(function() {
        var total = 0;
        ko.utils.arrayForEach(self.BoxScoreDetails(), function(boxScoreDetail) {
            total += parseFloat(boxScoreDetail.Hits());
        });
        return total.toFixed(0);
    }), 

    self.Total2B = ko.computed(function() {
        var total = 0;
        ko.utils.arrayForEach(self.BoxScoreDetails(), function(boxScoreDetail) {
            total += parseFloat(boxScoreDetail.Doubles());
        });
        return total.toFixed(0);
    }), 

    self.Total3B = ko.computed(function() {
        var total = 0;
        ko.utils.arrayForEach(self.BoxScoreDetails(), function(boxScoreDetail) {
            total += parseFloat(boxScoreDetail.Triples());
        });
        return total.toFixed(0);
    }), 

    self.TotalHR = ko.computed(function() {
        var total = 0;
        ko.utils.arrayForEach(self.BoxScoreDetails(), function(boxScoreDetail) {
            total += parseFloat(boxScoreDetail.HomeRuns());
        });
        return total.toFixed(0);
    }), 

    self.TotalBB = ko.computed(function() {
        var total = 0;
        ko.utils.arrayForEach(self.BoxScoreDetails(), function(boxScoreDetail) {
            total += parseFloat(boxScoreDetail.Walks());
        });
        return total.toFixed(0);
    }), 

    self.TotalRBI = ko.computed(function() {
        var total = 0;
        ko.utils.arrayForEach(self.BoxScoreDetails(), function(boxScoreDetail) {
            total += parseFloat(boxScoreDetail.RunsBattedIn());
        });
        return total.toFixed(0);
    })
};

$("form").validate({
    submitHandler: function () {
        gameViewModel.save();
    },
    rules: {
        Opponent: {
            required: true
        }
    }
});