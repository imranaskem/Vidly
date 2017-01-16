﻿$(document).ready(function () {
    let table = $("#movies").DataTable({
        ajax: {
            url: "/api/movies",
            dataSrc: ""
        },
        columns: [
            {
                data: "name",
                render: function (data, type, movie) {
                    return "<a href='/movies/edit/" + movie.id + "'>" + movie.name + "</a>"
                }
            },
            {
                data: "genre.name"
            },
            {
                data: "id",
                render: function (data) {
                    return "<button class='btn-link js-delete' data-movie-id=" + data + ">Delete</button>"
                }

            }
        ]
    });

    $("#movies").on("click", ".js-delete", function () {
        let button = $(this);

        bootbox.confirm("Are you sure you want to delete this movie?", function (result) {
            if (result) {
                $.ajax({
                    url: "/api/movies/" + button.attr("data-movie-id"),
                    method: "DELETE",
                    success: function () {
                        table.row(button.parents("tr")).remove().draw();
                    }
                });
            }
        });
    });
});