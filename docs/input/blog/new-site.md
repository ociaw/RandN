Title: New Site
Published: 2020-06-25
Author: ociaw
Category: Website
---

The [new site](https://randn.ociaw.com) for RandN is up - though if you're reading this, you probably
already know that. Built with Wyam, it's got this dedicated blog, an area for [handwritten
documentation](/docs), and [API documentation](/api) generated from the RandN source code.

I'm still playing around with handling documentation versioning - I'd like the docs for v0.1 to
still be available after v0.2 is released. Right now the main site tracks the `master` bookmark,
while [v0.1](/v0.1) will track `v0.1`, and so on.

Like the old site, i.e. my blog, it's statically generated and tries to be as lightweight as I can
make it. I removed a whole lotta unused CSS and JavaScript from the default theme, and then I took
it a step further and removed some that was still in use - namely FontAwesome (so if you see some
ugly icons or lack thereof, that's why) and Mermaid.js. Mermaid would have generated some fancy
class diagrams, but I decided it wasn't worth an entire megabyte. I did try to generate the
diagrams at build time, but was never able to get it working. There's still improvements to be
made, but since the theme is built on AdminLTE, which is in turn built on Bootstrap, which is built
on jQuery, I'd probably just have to write a brand new theme.

An important thing to note - at the moment, nested classes are not listed with their containing
classes - they're effectively orphans. You can however find them through the API search feature.

Lastly, this site will probably move over to [Statiq](https://statiq.dev/) once it's capable of
generating documentation easily enough.
