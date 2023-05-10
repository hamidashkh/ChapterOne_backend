$(document).ready(function () {
    $('#searchInput').keyup(function ()
    {
        let search = $(this).val();

        if (search.length >= 3) {
            fetch('product/search?search=' + search )
                .then(res => {
                    return res.text()
                }).then(data => {
                    $('#searchbody').html(data)
                })
        } else {
            $('#searchbody').html('')
        }
    })
})