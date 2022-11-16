
var CalendarViewModel = function (userID) {
    var self = this;

    self.userCalendarObject = ko.observable({
        eventID: ko.observable(),
        userID: ko.observable(),
        eventTitle: ko.observable(),
        eventDescription: ko.observable(),
        eventCreationDate: ko.observable(),
        eventStartTime: ko.observable(),
        eventEndTime: ko.observable(),
        eventIsFavorited: ko.observable()
    });

    self.userEvents = ko.observableArray([]);

    config = {
        enableTime: true,
        dateFormat: "Y-m-d H:i",
    }

    flatpickr("input[type=datetime-local]", config);

    self.dismissEditModal = function () {
        $("#editEventModal").modal("toggle");
    }

    self.getUserEvents = function () {
        $.ajax({
            url: "/Home/events/?userID=" + userID,
            type: "GET",
            success: function (data) {
                $.each(data, function (i, v) {
                    self.userEvents.push({
                        eventid: v.eventID,
                        userid: v.userID,
                        title: v.eventTitle,
                        description: v.eventDescription,
                        start: moment(v.eventStartTime),
                        end: v.eventEndTime != null ? moment(v.eventEndTime) : null,
                        color: v.color,
                        favorited: v.eventIsFavorited
                    })
                })
                GenerateCalendar(self.userEvents());
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        })
    }

    function GenerateCalendar(userEvents) {
        $('#calendar').fullCalendar('Drop');
        $('#calendar').fullCalendar({
            contentHeight: 400,
            defaultDate: new Date(),
            timeFormat: 'h(:mm)a',
            header: {
                left: 'prev, next today',
                center: 'title',
                right: 'month, basicWeek, basicDay,agenda'
            },
            eventLimit: true,
            eventColor: '#378006',
            events: userEvents,
            eventClick: function (calEvent, jsEvent, view) {
                $('#DBModal #eventTitle').text(calEvent.title);
                var $description = $('<div/>');
                $description.append($('<p/>').html('<b>Start:</b>' + calEvent.start.format("DD-MMM-YYYY HH:mm a")));
                if (calEvent.end != null) {
                    $description.append($('<p/>').html('<b>End:</b>' + calEvent.end.format("DD-MMM-YYYY HH:mm a")));
                }
                $description.append($('<p/>').html('<b>Description:</b>' + calEvent.description));
                $('#DBModal #pDetails').empty().html(description);
                $('#DBModal').modal();
            }
        })
    }

    self.createUserEvent = function (eventData) {
        console.log(JSON.stringify(eventData));
        $.ajax({
            url: "/Home/events",
            type: "POST",
            datatype: "json",
            contentType: "application/json",
            data: JSON.stringify(eventData),
            success: function (result) {
                if (result == -1) {
                    toastr.error("Event was not successfully created");
                } else {
                    toastr.success("Event was created");
                    self.getUserEvents();
                }
            },
            error: function (result) {
                toastr.error("There has been an error");
                console.log(result)
            }
        })
    }

    self.submitUserEvent = function () {
        var EventTitle = $("#eventTitle").val();
        var EventDescription = $("#eventDescription").val();
        var EventStartTime = new Date($("#eventStartTime").val());
        var newEventStartTime = new Date(EventStartTime.getTime() - (EventStartTime.getTimezoneOffset() * 60000)).toJSON();
        var EventEndTime = new Date($("#eventEndTime").val());
        var newEventEndTime = new Date(EventEndTime.getTime() - (EventEndTime.getTimezoneOffset() * 60000)).toJSON();
        let payload = {
            userID: userID,
            eventTitle: EventTitle,
            eventDescription: EventDescription,
            eventStartTime: newEventStartTime,
            eventEndTime: newEventEndTime
        };
        console.log(payload);
        self.createUserEvent(payload);
        $("#createEventModal").modal("toggle");
        return;
    }

    var Id;
    self.editevent = function (Object) {
        ko.mapping.fromJS(Object, {}, self.userCalendarObject);
        Id = Object.eventID;
    }

    self.submitEditedUserEvent = function () {
        var eventID = Id;
        var eventTitle = $("#editEventTitle").val();
        var eventDescription = $("#editEventDescription").val();

        let payload2 = {
            userId: userID,
            eventId: eventID,
            eventtitle: eventTitle,
            eventdescription: eventDescription
        }

        self.updateEvent(payload2);
    }

    self.updateEvent = function (payload2) {
        $.ajax({
            url: "/Home/events",
            type: "PUT",
            dataType: "json",
            contentType: "application/json",
            data: JSON.stringify(payload2),
            success: function (result) {
                if (result == -1) {
                    toastr.error("Error");
                } else {
                    toastr.success("Successfully Changed Event");
                    self.getUserEvents();
                    self.dismissEditModal();
                }
            },
            error: function () {
                toastr.error("Error Updating Event")
            }
        })
    }

    self.deleteEvent = function (Object) {
        $.ajax({
            url: '/Home/events/' + Object.eventID,
            type: 'DELETE',
            success: function () {
                self.getUserEvents();
                toastr.success("Event was deleted");
            },
            error: function (jqXHR) {
                toastr.error(jqXHR.responseText);
            }
        })
    }
}