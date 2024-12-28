// Caution! Be sure you understand the caveats before publishing an application with
// offline support. See https://aka.ms/blazor-offline-considerations

self.importScripts('./service-worker-assets.js');

self.addEventListener('install', event => event.waitUntil(onInstall(event)));
self.addEventListener('activate', event => event.waitUntil(onActivate(event)));
self.addEventListener('fetch', event => event.respondWith(onFetch(event)));

const cacheNamePrefix = 'offline-cache-';
const cacheName = `${cacheNamePrefix}${self.assetsManifest.version}`;
const offlineAssetsInclude = [
    /\.dll$/, /\.pdb$/, /\.wasm/, /\.html/, /\.js$/, /\.json$/, /\.css$/,
    /\.woff$/, /\.woff2$/, /\.png$/, /\.jpe?g$/, /\.gif$/, /\.ico$/, /\.blat$/, /\.dat$/
];
const offlineAssetsExclude = [/^service-worker\.js$/];

// Replace with your base path if you are hosting on a subfolder. Ensure there is a trailing '/'.
const base = "/";
const baseUrl = new URL(base, self.origin);
const manifestUrlList = self.assetsManifest.assets.map(asset => new URL(asset.url, baseUrl).href);

async function onInstall(event) {
    console.info('Service worker: Install');
    try {
        const assetsRequests = self.assetsManifest.assets
            .filter(asset => offlineAssetsInclude.some(pattern => pattern.test(asset.url)))
            .filter(asset => !offlineAssetsExclude.some(pattern => pattern.test(asset.url)))
            .map(asset => new Request(asset.url, { integrity: asset.hash, cache: 'no-cache' }));

        const cache = await caches.open(cacheName);
        await cache.addAll(assetsRequests);
        console.info('Assets cached successfully.');
    } catch (error) {
        console.error('Error during service worker install:', error);
    }
}

async function onActivate(event) {
    console.info('Service worker: Activate');
    try {
        const cacheKeys = await caches.keys();
        await Promise.all(cacheKeys
            .filter(key => key.startsWith(cacheNamePrefix) && key !== cacheName)
            .map(key => caches.delete(key)));
        console.info('Old caches cleared.');
    } catch (error) {
        console.error('Error during service worker activation:', error);
    }
}

async function onFetch(event) {
    try {
        let cachedResponse = null;

        if (event.request.method === 'GET') {
            const shouldServeIndexHtml = event.request.mode === 'navigate'
                && !manifestUrlList.some(url => url === event.request.url)
                && !event.request.url.includes('/api/'); // Exclude API calls

            const request = shouldServeIndexHtml ? 'index.html' : event.request;

            const cache = await caches.open(cacheName);
            cachedResponse = await cache.match(request);
        }

        if (!cachedResponse) {
            const fetchResponse = await fetch(event.request);
            if (event.request.method === 'GET') {
                const cache = await caches.open(cacheName);
                cache.put(event.request, fetchResponse.clone());
            }
            return fetchResponse;
        }

        return cachedResponse;
    } catch (error) {
        console.error('Error during fetch:', error);
        return fetch(event.request).catch(() => {
            // Optionally, return a fallback response here if fetch fails
        });
    }
}
