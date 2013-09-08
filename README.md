ToKO
====

ToKO is a set of helpers to help you convert your view model to a knockout view model in asp.net. 

This is a work in progress, use with caution.

Examples
--------

See below

In short : 

```
<script type="text/javascript">
	ko.applyBindings(@Html.Raw(Model.ToKO().ToJavascript()));
</script>
```

You can also you `ToJavascriptObject()` to create an object, which you can then extend to add computed properties and such. Intended primarily at coffeescript users.

Todo 
----

* Add examples
* Generate readable code
* Generate minified code
* Use MvcHtmlString or whatever that thing is called
* Nugetise
