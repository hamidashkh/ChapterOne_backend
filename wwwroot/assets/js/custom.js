$(document).ready(function () {
    $(".addToBasket").click(function myfunction(e) {
        e.preventDefault();

        let productId = $(this).data('id');

        fetch('basket/AddBasket?id=' + productId)
            .then(res => {
                return res.text();
            }).then(data => {
                console.log(data);

                //$('.header-cart').html(data);
            })
    })

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