function calculate() {


    StartDate1 = document.theForm.StartDate1.value;
    StartDate2 = document.theForm.StartDate2.value;
    total = StartDate1 + " " + StartDate2;
    document.theForm.StartDate.value = total;

    EndDate1 = document.theForm.EndDate1.value;
    EndDate2 = document.theForm.EndDate2.value;
    total2 = EndDate1 + " " + EndDate2;
    document.theForm.EndDate.value = total2;

}