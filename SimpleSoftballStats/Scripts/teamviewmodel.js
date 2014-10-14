var ObjectState = {
    Unchanged: 0,
    Added: 1,
    Modified: 2,
    Deleted: 3
}

var teamRosterEntryMapping = {
    'RosterEntries': {
        key: function(rosterEntry) {
            return ko.utils.unwrapObservable(rosterEntry.PlayerId);
        }, 
        create: function(options) {
            return new RosterEntryViewModel(options.data);
        }
    }
}

RosterEntryViewModel = function (data) {
    var self = this;
    ko.mapping.fromJS(data, teamRosterEntryMapping, self);

    self.PlayerFullName = ko.computed(function () {
        return self.Player.FirstName() + " " + self.Player.LastName();
    });
}

var playerMapping = {
    'AvailablePlayers': {
        key: function (player) {
            return ko.utils.unwrapObservable(player.PlayerId);
        }, 
        create: function(options) {
            return new PlayerViewModel(options.data);
        }        
    }
}

PlayerViewModel = function (data) {
    var self = this;
    ko.mapping.fromJS(data, playerMapping, self);
}

TeamViewModel = function (data) {
    var self = this;
    ko.mapping.fromJS(data, teamRosterEntryMapping, self);

    self.save = function () {
        $.ajax({
            url: "/Teams/Save/",
            type: "POST",
            data: ko.toJSON(self),
            contentType: "application/json",
            success: function (data) {
                if (data.teamViewModel != null)
                    ko.mapping.fromJS(data.teamViewModel, {}, self);

                if (data.newLocation != null)
                    window.location = data.newLocation;
            }
        });
    },

    self.flagTeamAsEdited = function () {

        if (self.ObjectState() != ObjectState.Added) {
            self.ObjectState(ObjectState.Modified);
        }

        return true;
    }

    self.addPlayerToTeam = function () {
        var rosterEntryViewModel = new RosterEntryViewModel({ PlayerId: self.PlayerToAdd().PlayerId(), Player: self.PlayerToAdd(), TeamId: this.TeamId(), ObjectState: ObjectState.Added });
        self.RosterEntries.push(rosterEntryViewModel);
    }

    self.deleteRosterEntry = function (rosterEntry) {
        self.RosterEntries.remove(this);        
        if (self.RosterEntriesToDelete.indexOf(rosterEntry.PlayerId()) == -1) {
            self.RosterEntriesToDelete.push(rosterEntry.PlayerId);
        }
    }
};

$("form").validate({    
    submitHandler: function () {        
        teamViewModel.save();
    },
    rules: {
        TeamName: {
            required: true
        }
    }
});