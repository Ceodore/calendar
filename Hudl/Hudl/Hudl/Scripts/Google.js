addEvent = function (authResult, artist, description, date, startTime, endTime, address) {
    var clientId = '1071645370589-acdsg7rjbsk7dn5lecfbk83k9uh8fnaa.apps.googleusercontent.com';

    var apiKey = 'AIzaSyCM9baxkHRUf9uGzgW9UMzu3CzV-Urqw0g';

    var scopes = 'https://www.googleapis.com/auth/plus.login';

    handleClientLoad(apiKey);
    checkAuth(clientId, scopes);
    handleAuthResult(authResult);
    if (authResult != null && !authResult.error && authResult['status']['signed_in']) {
        getCalendarId(artist, description, date, startTime, endTime, address);
    } else {
        displayError();
    }
};

var calendarId;
var timeZone;
var status;

function handleClientLoad(apiKey) {
    // Step 2: Reference the API key
    gapi.client.setApiKey(apiKey);
    window.setTimeout(checkAuth, 1);
};

function checkAuth(clientId, scopes) {
    gapi.auth.authorize({ client_id: clientId, scope: scopes, immediate: true }, handleAuthResult);
}

function handleAuthResult(authResult) {
    var authorizeButton = document.getElementById('signinButton');
    if (authResult && !authResult.error) {
        authorizeButton.style.visibility = 'hidden';
    } else {
        authorizeButton.style.visibility = '';
        authorizeButton.onclick = handleAuthClick;
    }
}

function handleAuthClick(clientId, scopes) {
    // Step 3: get authorization to use private data
    gapi.auth.authorize({ client_id: clientId, scope: scopes, immediate: false }, handleAuthResult);
    return false;
}

// Load the API and make an API call.  Display the results on the screen.
function getCalendarId(artist, description, date, startTime, endTime, address) {
    // Step 4: Load the Google+ API
    gapi.client.load('calendar', 'v3', function() {
        // Step 5: Assemble the API request
        var request = gapi.client.calendar.calendars.get({
            'calendarId': 'primary'
        });
        // Step 6: Execute the API request
        request.execute(function (resp) {
            if (resp != null && resp.id != null) {
                calendarId = resp.id;
                timeZone = resp.timeZone;
                addEventToCalendar(artist, description, date, startTime, endTime, address);
            } else {
                displayFailed();
            }
        });
    });
}

function addEventToCalendar(artist, description, date, startTime, endTime, address) {
    // Step 4: Load the Google+ API
    gapi.client.load('calendar', 'v3', function () {
        // Step 5: Assemble the API request
        var start = date + "T" + startTime;
        var end = date + "T" + endTime;
        var object = {
            'end': {
                'dateTime': end,
                'timeZone': timeZone
            },
            'start': {
                'dateTime': start,
                'timeZone': timeZone
            },
            'summary': artist,
            'description': description,
            'location': address
        };
        var calendarObject =
        {
            'calendarId': calendarId,
            'resource': object
        };
        var request = gapi.client.calendar.events.insert(calendarObject);
        //Step 6: Execute the API request
        request.execute(function (resp) {
            if (resp.status == "confirmed") {
                displaySuccess();
            } else {
                displayFailed();
            }
        });
    });
}