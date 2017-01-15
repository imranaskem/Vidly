$(document).ready(function () {
    let table = $("#rentals").DataTable({
        ajax: {
            url: "/api/rental",
            dataSrc: ""
        },
        columns: [
            {
                data: "movieName"              
            },
            {
                data: "customerName"
            },
            {
                data: "dateRented"
            },
            {
                data: "id",
                render: function (data) {
                    return "<button class='btn-link js-returned' data-rental-id=" + data + ">Returned</button>"
                }
            }
        ]
    });

    $("#rentals").on("click", ".js-returned", function () {
        let button = $(this);

        bootbox.confirm("Are you sure you want to return this movie?", function (result) {
            if (result) {
                $.ajax({
                    url: "/api/rental/" + button.attr("data-rental-id"),
                    method: "POST",
                    success: function () {
                        table.row(button.parents("tr")).remove().draw();
                    }
                });
            }
        });
    });
});