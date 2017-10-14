var map;
ymaps.ready(function() {
    map = new ymaps.Map('map', {
        center: [55.751574, 37.573856],
        zoom: 9,
        controls: []
    }, {
        searchControlProvider: 'yandex#search'
    });
});

map.controls.add( control.SearchControl, {
    float: 'none',
    position: {
        top: 10,
        left: 10
    }
});
