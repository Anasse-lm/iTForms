function initializeSortable(container, dotnetHelper) {
    new Sortable(container, {
        animation: 150,
        onEnd: function (evt) {
            dotnetHelper.invokeMethodAsync("UpdateQuestionOrder", evt.oldIndex, evt.newIndex);
        }
    });
}