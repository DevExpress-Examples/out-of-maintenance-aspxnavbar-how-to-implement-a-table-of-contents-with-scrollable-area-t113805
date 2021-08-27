<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128555035/13.2.9%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T113805)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [scripts.js](./CS/Content/scripts.js) (VB: [scripts.js](./VB/Content/scripts.js))
* [Default.aspx](./CS/Default.aspx) (VB: [Default.aspx](./VB/Default.aspx))
* [Default.aspx.cs](./CS/Default.aspx.cs) (VB: [Default.aspx.vb](./VB/Default.aspx.vb))
<!-- default file list end -->
# ASPxNavBar - How to implement a table of contents with scrollable area
<!-- run online -->
**[[Run Online]](https://codecentral.devexpress.com/t113805/)**
<!-- run online end -->


<p>This example demonstrates how to implement a table of contents with the scrollable area using the ASPxNavBar control. An xml file is used as a data source here, however it is possible to do the same with any other data source.<br />The main idea is to add items both in ASPxNavBar and the scrollable area when you process an entry from the data source.<br /><br />Once items are added, implement the function that will:<br />1) Determine an element that should be selected as an active item in the navbar:</p>


```js
var firstFullyVisibleIndex = 0;

$(".section").each(function () {
    var scrollOffset = $(document).scrollTop();
    var elementOffset = $(this).offset().top;
    if (scrollOffset < elementOffset) {
        firstFullyVisibleIndex = $(this).index();
        return false;
    }
});
```


2) Select this item in the navbar:<br />


```js
if (selectedSectionIndex != firstFullyVisibleIndex) {
    selectedSectionIndex = firstFullyVisibleIndex;
    var group = navbar.GetActiveGroup();
    var newlySelectedItem = group.GetItem(selectedSectionIndex);
    navbar.SetSelectedItem(newlySelectedItem);
}
```


Call this function in two event handlers: when the page loads and when the page is scrolled:<br />


```js
$(window).scroll(function () {
    SyncNavbar();
});
$().ready(function () {
    navbar.SetActiveGroup(navbar.GetGroupByName(navbar.cpActiveGroup));
    SyncNavbar();
});
```



<br/>


