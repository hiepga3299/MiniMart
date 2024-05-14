function mapObjectToControllerView(modelView) {
    if (typeof modelView !== 'object') {
        return;
    }
    for (var property in modelView) {
        if (modelView.hasOwnProperty(property)) {
            const [firtCharacter, ...restChar] = property;
            const capitalChar = `${firtCharacter.toLocaleUpperCase()}${restChar.join('')}`;
            $(`#${capitalChar}`).val(modelView[property]);
        }
    }
}

function showToaster(type, text, timeOut = 5000) {
    $.toast({
        heading: type,
        text: text,
        icon: type === 'Information' ? 'info' : type.toLowerCase(),
        position: 'top-right',
        hideAfter: timeOut
    })
}