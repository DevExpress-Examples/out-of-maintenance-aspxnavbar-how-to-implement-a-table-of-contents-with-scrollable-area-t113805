var selectedSectionIndex = -1;

var scrollTo = function (selector) {
    $('html, body').animate({ scrollTop: $(selector).offset().top - 50}, 750);
}

function OnNavBarItemClick(s,e){
    var itemIndex = e.item.index;
    scrollTo(".section:eq(" + itemIndex + ")");
}
function OnHeaderClick(s, e) {
    var queryParameter = $("a", e.htmlElement).attr('href');
    if (window.location.search != queryParameter)
        window.location.search = queryParameter;
    else
        e.cancel = true;
}

function SyncNavbar() {
    var firstFullyVisibleIndex = 0;

    $(".section").each(function () {
        var scrollOffset = $(document).scrollTop();
        var elementOffset = $(this).offset().top;
        if (scrollOffset < elementOffset) {
            firstFullyVisibleIndex = $(this).index();
            return false;
        }
    });

    if (selectedSectionIndex != firstFullyVisibleIndex) {
        selectedSectionIndex = firstFullyVisibleIndex;
        var group = navbar.GetActiveGroup();
        var newlySelectedItem = group.GetItem(selectedSectionIndex);
        navbar.SetSelectedItem(newlySelectedItem);
    }
}

$(window).scroll(function () {
    SyncNavbar();
});

$().ready(function () {
    navbar.SetActiveGroup(navbar.GetGroupByName(navbar.cpActiveGroup));
    SyncNavbar();
});