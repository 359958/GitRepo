$(document).ready(function () {
    $('#btnClick').click(createUser);
    //$('#btnLogin').click(loginUser);
    $('#btnAdd').click(AddMovieDetails);
   // $('#btnBook').click(BookMovie);
    $('#btnSinup').click(Movopage);
    DatePicker();
    $('#MovieID').change(function () {

        var selected = $('option:selected').text();
        alert(selected);
        DateLoad(selected);
    });
});

function DatePicker() {
    var date_input = $('input[name="datepicker"]'); //our date input has the name "date"
    var container = $('.bootstrap-iso form').length > 0 ? $('.bootstrap-iso form').parent() : "body";
    var options = {
        format: 'mm/dd/yyyy',
        container: container,
        todayHighlight: true,
        orientation:"bottom right",
        autoclose: true
    };
    date_input.datepicker(options);
}

function DateLoad(movie)
    {
    $.ajax({
        type: "POST",
        url: "http://localhost:60683/api/BookMovie/MovieDate",
        contentType: "application/json; charset=utf-8",
        data: { "movie": movie },
        success: function (data) {
            $('#datepicker').html('');
            var options = '';
            options += '<option value="Select">Select</option>';
            for (var i = 0; i < response.length; i++) {
                options += '<option value="' + response[i] + '">' + response[i] + '</option>';
            }
            $('#datepicker').append(options); 
        }
        ,
        error: function (response) {
            alert('bye');
        }
    });
    }

function Ticket() {

}

function createUser() {
    var Customer = {
        CName: $("#CName").val(),
        CPhone: $("#CPhone").val(),
        Cemail: $("#Cemail").val(),
        Password: $("#Password").val()
    };

    if (Customer != null) {
        $.ajax({
            type: "POST",
            url: "http://localhost:60683/api/Register/createUser",
            data: Customer,
            success: function (data) {
                alert('hi');
            }
            ,
            error: function (response) {
                alert('bye');
            }
        });
    }
}

function AddMovieDetails() {
    var AddMovie = {
        Screen: $("#Screen").val(),
        Movie: $("#MovieName").val(),
        RunningUpto: $("#date").val(),
        From: $("#from").val()
        
    };
   
    if (AddMovie != null) {
        $.ajax({
            type: "POST",
            url: "http://localhost:60683/api/Admin/AddMovie",
            data: AddMovie,
            success: function (data) {
                alert('Movie Added');
            }
            ,
            error: function (response) {
                alert('Failed');
            }
        });
    }
}

function loginUser() {
   // var newUrl = '@Url.Action("BookMovie","BookMovie")';
    var UsersLogin = {
        UserName: $("#UserName").val(),
        Password: $("#Password").val(),
    };

    if (UsersLogin != null) {
        $.ajax({
            type: "POST",
            url: "http://localhost:60683/api/Register/loginUser",
            data: UsersLogin,
            success: function (data) {
                window.location.href = '@Url.Action("BookMovie","BookMovie")';;
            }
            ,
            error: function (response) {
                alert('bye');
            }
        });
    }
}

function BookMovie() {

    var BookMovieData = {
        Movieid: $("#MovieID").val(),
        Classid: $("#Classid").val(),
        BookingDate: $("#date").val(),
        Showid: $("#Showid").val(),
        NoTickets: $("#NoTickets").val(),
        CUSTOMERID: 25
    };
    alert(BookMovieData.BookingDate);
    
    if (BookMovieData != null) {
        $.ajax({
            type: "POST",
            url: "http://localhost:60683/api/BookMovie/BookTicket",
            data: BookMovieData,
            success: function (data) {
                alert('hi');
            }
            ,
            error: function (response) {
                alert('bye');
            }
        });
    }

}

function Movopage()
{
    alert();
    document.location = '@Url.Action("CreateUser","Register")';
   
}


//$("#btnSave").click(function () {
//    debugger
//    var Customer = {
//        CName: $("#CName").val(),
//        CPhone: $("#CPhone").val(),
//        Cemail: $("#Cemail").val(),
//        Password: $("#Password").val()
//    };
//    //Customer.Name = $("#CName").val();
//    //Customer.Phone = $("#CPhone").val();
//    //Customer.email = $("#Cemail").val();
//    //alert(Customer.email)
//    if (Customer != null) {
//        $.ajax({
//            type: "POST",
//            url: "http://localhost:55982/api/Values/PostAdd",
//            data: Customer,
//            success: function (data) {
//                alert('hi');
//            }
//            ,
//            error: function (response) {
//                alert(response.responseText);
//            }
//        });
//    }
//});