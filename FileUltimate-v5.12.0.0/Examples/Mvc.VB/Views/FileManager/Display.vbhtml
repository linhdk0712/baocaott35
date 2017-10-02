﻿@Imports GleamTech.Web.Mvc
@Imports GleamTech.FileUltimate
@ModelType FileManager()

<!DOCTYPE html>

<html>
<head>
    <title>Display</title>
    @Html.RenderCss(Model(0))
    @Html.RenderJs(Model(0))
</head>
<body style="margin: 20px;">

    1. FileManager instance displayed as inline element:
    <input type="button" value="Show" onclick="fileManager1.show()" />
    <input type="button" value="Hide" onclick="fileManager1.hide()" />
    <br /><br />
    @Html.RenderControl(Model(0))

    2. FileManager instance displayed as a modal dialog of viewport:
    <input type="button" value="Show" onclick="fileManager2.show()" />
    <br /><br />
    @Html.RenderControl(Model(1))

    3. FileManager instance displayed as a modal dialog of parent element:
    <input type="button" value="Show" onclick="fileManager3.show()" />
    <input type="button" value="Hide" onclick="fileManager3.hide()" />
    <br /><br />
    <div style="width: 1000px; height: 800px; border: 1px dashed black">
        Parent &lt;div&gt; element
        @Html.RenderControl(Model(2))
    </div>

</body>
</html>
