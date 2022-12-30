// Open/Close large image detail of product
$(document).ready(function () {
    $(".smallImageDetail").click(function () {
        var path = $(this).attr('src');
        $("#largeImageDetail").attr('src', path);
        $(".overlay").show();
        document.getElementById("body").classList.toggle("modal-open");
    })

    $("#closeBtn").click(function () {
        $(".overlay").hide();
        document.getElementById("body").classList.remove("modal-open");
    })
})

//Show sort publisher in index page
window.onload = function () {
    getPage();
    listSort();
};

function getPage() {
    var path = window.location.pathname;
    var page = path.split("/").pop();

    if (page != "") {
        $(".hideNavItem").hide();
    }
    else {
        $(".hideNavItem").show();
    }
}

//Manage sort list
function listSort() {
    if (window.location.pathname.includes("/Home/Store")) {
        var path = window.location.search;
        var page = path.replace("?sortOrder=", "");
        page = page.substring(0, page.lastIndexOf('&')).toUpperCase();
        var element = $("#SortSlect");

        switch (page) {
            case "NAMEA":
                element.val('NameA')
                break;
            case "NAMEB":
                element.val('NameB')
                break;
            case "PRICEA":
                element.val('PriceA')
                break;
            case "PRICEB":
                element.val('PriceB')
                break;
            case "ALLPRICE":
                document.getElementById('ALLPRICE').classList.toggle("active")
                break;
            case "SECTIONONEPRICE":
                document.getElementById('SECTIONONEPRICE').classList.toggle("active")
                break;
            case "SECTIONTWOPRICE":
                document.getElementById('SECTIONTWOPRICE').classList.toggle("active")
                break;
            case "SECTIONTHREEPRICE":
                document.getElementById('SECTIONTHREEPRICE').classList.toggle("active")
                break;
            case "SECTIONFOURPRICE":
                document.getElementById('SECTIONFOURPRICE').classList.toggle("active")
                break;
            default:
                element.val('Aucun')
                break;
        }
    }
}

//Manage Filter/Sort
function sortSelect(choice, pageNumber) {
    window.location.href = "Store?sortOrder=" + choice.replace(" ", "") + "&page=" + pageNumber;
}

//Upgrate adaptation searchBar
function search() {
    var choice = document.getElementById("searchBarID").value;
    while (choice.includes(' ')) {
        choice = choice.replace(' ', '');
    }
    var href = "/Home/Store?sortOrder=" + "SEARCH:" + choice + "&page=1";
    window.location.href = window.location.origin + href;
}
var input = document.getElementById("searchBarID");
input.addEventListener("keypress", function (event) {
    if (event.key === "Enter") {
        event.preventDefault();
        search();
    }
});

//Open sub-menu of user account
function openUserMenu() {
    document.getElementById("userMenuDropdown").classList.toggle("show");
}
window.onclick = function (event) {
    if (!event.target.matches('.userbtn')) {
        var dropdowns = document.getElementsByClassName("userMenu-content");
        var i;
        for (i = 0; i < dropdowns.length; i++) {
            var openDropdown = dropdowns[i];
            if (openDropdown.classList.contains('show')) {
                openDropdown.classList.remove('show');
            }
        }
    }
}

//Show popup if add product in cart
function showPopup() {
    var popup = document.getElementById("popupAddCart");
    popup.classList.toggle("show");

    sleep(750);
};

//Simulate sleep time
function sleep(milliseconds) {
    var start = new Date().getTime();
    for (var i = 0; i < 1e7; i++) {
        if ((new Date().getTime() - start) > milliseconds) {
            break;
        }
    }
}

//Manage toogle in account profil
function menuAccountProfil(choice) {
    if (document.getElementById("informationAccount").classList.contains('showElement')) {
        document.getElementById("informationAccount").classList.remove('showElement');
    }
    if (document.getElementById("orderAccount").classList.contains('showElement')) {
        document.getElementById("orderAccount").classList.remove('showElement');
    }
    if (document.getElementById("otherAccount").classList.contains('showElement')) {
        document.getElementById("otherAccount").classList.remove('showElement');
    }

    if (document.getElementById("choiceAccount1").classList.contains('active')) {
        document.getElementById("choiceAccount1").classList.remove('active');
    }
    if (document.getElementById("choiceAccount2").classList.contains('active')) {
        document.getElementById("choiceAccount2").classList.remove('active');
    }
    if (document.getElementById("choiceAccount3").classList.contains('active')) {
        document.getElementById("choiceAccount3").classList.remove('active');
    }

    choice = choice.replace("'", "\"");

    switch (choice) {

        case "informationAccount":
            document.getElementById("choiceAccount1").classList.toggle("active");
            break;
        case "orderAccount":
            document.getElementById("choiceAccount2").classList.toggle("active");
            break;
        case "otherAccount":
            document.getElementById("choiceAccount3").classList.toggle("active");
            break;
        default:
            document.getElementById("choiceAccount1").classList.toggle("active");
            break;
    }

    document.getElementById(choice).classList.toggle("showElement");

}

//Show good form in cart
function ChooseAddress(choice) {
    if (document.getElementById("newAddressShop").classList.contains('showElement')) {
        document.getElementById("newAddressShop").classList.remove('showElement');
    }
    if (document.getElementById("oldAddressShop").classList.contains('showElement')) {
        document.getElementById("oldAddressShop").classList.remove('showElement');
    }

    document.getElementById(choice).classList.toggle("showElement");

    if (choice == "newAddressShop") {
        document.getElementById("addressN").checked = true;
        document.getElementById("addressO").checked = false;
    }
    else if (choice == "oldAddressShop") {
        document.getElementById("addressN").checked = false;
        document.getElementById("addressO").checked = true;
    }

}

function CheckOptionnalOption() {
    var conceptName = $('#categoriesList').find(":selected").text();

    if (conceptName == "Jeu") {
        document.getElementById("additionnalOption").classList.toggle("showElement");
    }
    else {
        document.getElementById("additionnalOption").classList.remove('showElement');
    }
}

$(document).ready(function () {

    $("#cancel-btn").on("click", function () {
        var x = document.getElementById("payment-section");
        x.close();
        document.getElementById("body").classList.remove("modal-open");
    });

});

//Animation to add on cart in store page
function AnimationCheck(id) {
    var one = "wrapper " + id
    document.getElementById(one).classList.toggle("wrapper");
    var two = "checkmark " + id
    document.getElementById(two).classList.toggle("checkmark");
    var three = "checkmark__circle " + id
    document.getElementById(three).classList.toggle("checkmark__circle");
    var four = "checkmark__check " + id
    document.getElementById(four).classList.toggle("checkmark__check");
}