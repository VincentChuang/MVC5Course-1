編輯 Edit  Template 部份，含 jQuery DatePicker 效果：

- 在 Views/Shared/EditorTemplates 下建立
「Date_DisplayEditor_Template.cshtml」檔案內容
@model Nullable<System.DateTime>
@if (Model.HasValue) {
    @Html.TextBox("", Model.Value.ToString("yyyy-MM-dd"), new { @class = "form-control text-box single-line" })
} else {
    @Html.TextBox("", null, new { @class = "form-control text-box single-line" })
}
<script>
    $(function () {
        $("#@Html.Id("")").datepicker({ //利用 後端語法 取得 前端ID
            dateFormat: "yy-mm-dd"
        });
  })
</script>



- 在 Views/Shared/DisplayTemplates 下建立
「Date_DisplayEditor_Template.cshtml」檔案內容：
@model Nullable<System.DateTime>
@if (Model.HasValue) {
  @(((DateTime)Model).ToString("yyyy-MM-dd"))
}

- 修改 PartialClass 其 Gender 屬性 ，以套用 Template

- [UIHint("Date_DisplayEditor_Template")] //套用編輯Template
public Nullable<System.DateTime> DateOfBirth { get; set; }

顯示 Display  Template 部份：

- 在 Views/Shared/DisplayTemplate 下建立
「Gender_Template.cshtml」內容
@model string
@if (Model != null) {
    if (Model == "M") {
      @:男性
    } else {
      @:女性
    }
}
- 「CreditRating_Template.cshtml」
@model double?
   @if (Model != null && Model.HasValue) {
      for (int i = 0; i < Model.Value; i++) {
         @("🌠") //由「https://emojipedia.org/」網站提供的 unicode 文字
    }
 }

- 修改 PartialClass 其 Gender 屬性 ，以套用 Template

- [UIHint("Gender_Template")]     //指定 View_DisplayTemplate 檔名
public string Gender { get; set; }

- 修改 PartialClass 其 CreditRating 屬性，以套用 Template
[UIHint("CreditRating_Template")]
public Nullable<double> CreditRating { get; set; }