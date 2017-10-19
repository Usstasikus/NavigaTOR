var map;
ymaps.ready(function() {
    map = new ymaps.Map('map', {
        center: [55.751574, 37.573856],
        zoom: 9,
        controls: ['searchControl']
    }, {
        searchControlProvider: 'yandex#search'
    });
});
