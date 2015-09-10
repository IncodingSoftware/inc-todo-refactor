<h1> Refactor Version</h1>
<p style="text-align: center;"><img class="aligncenter  wp-image-1339" src="http://blog.incframework.com/wp-content/uploads/2014/03/Article4_text_Small2.jpg" alt="angular vs iml 2" width="800" height="400" /></p>
<strong>disclaimer</strong>: this <a title="AngularJs vs IML" href="http://blog.incframework.com/angularjs-vs-iml/">article</a> is a rejoinder (that was highly critical on <a href="http://habrahabr.ru/post/214293/">Habr</a>) and fulfil a potential of IML on the example of popular app <a href="http://todomvc.com">ToDo MVC</a>.
Critic is not the word for the discussion concerning my previous article. It looked like an assault and there were unpleasant comments (ludicrous Top on the picture), moreover even impartial:
<ul>
	<li><strong>Code on AngularJs is no great shakes </strong><b>фонтан</b>  - – difficult to disprove, although they are all from web-site and popular instruction</li>
	<li><strong>Weak examples</strong> - we stressed on the tasks, not on the scripts. But I agree that more complex decision fulfils a potential (I offered some our projects that are available on “open source”, but they were ignored)</li>
	<li><strong>Do not know AngularJs ?</strong> -  for obvious reasons it’s hurt the developers <strong>AngularJs</strong></li>
	<li><strong>Topic JS</strong> - this is an grave error, because if you don’t use asp.net mvc it’s difficult to understand possibilities of typified <strong>TextBoxFor</strong> or other add-ons.</li>
</ul>
<h1>Why ToDo ?</h1>
In comments there were an offer to realize “Todo MVC” as an proof of IML possibilities. There is a <a href="http://todo.incframework.com/">result</a>. First of all, in contrast to js framework, test-version doesn’t use as a container “local storage”, but use data-bases and <a href="https://github.com/IncodingSoftware/inc-todo">source code</a>. We will consider it more. In the process of realization I built the whole of logistics (basement computation, hiding elements and etc.) on the client, although on the tasks. It’s easier (sometimes necessary) to update elements “pointwise”, that with IML-code know how to compute and reflect themselves
<h1>Code review</h1>
Today we will not compare 2 variants of decision (in this case the volume of material will be very large). We’ll make a review of a code, which we’ll get in the realization “todo” app. I’ve mentioned above that in the IML realization is the server side, but in order to equalize the task for more objective comparison, we focus only on client’s side
<h2>What it consists of</h2>
The code was splited into 3 View
<ul>
	<li><strong>Index - </strong>main page ( actuallu it is single page )</li>
	<li><strong>Todo_List_Tmpl</strong> - a pattern for central list</li>
	<li><strong>Todo_Footer_Tmpl</strong> - – a pattern for building a basement with</li>
</ul>
[caption id="" align="aligncenter" width="884"]<img title="Location in graphical form" src="http://content.screencast.com/users/VladA4/folders/Snagit/media/2be73422-e54b-44d0-a01c-94445125d251/03.05.2014-19.20.png" alt="" width="884" height="393" /> Location in graphical form[/caption]

<strong><span style="color: #ff0000;">Dangerous: further will be a lot of C#-code and the lack of JavaScript, so those who are nervous, please, calm down and don’t get scared at every “using(Html)”</span></strong>
<h4>The form for TODO adding</h4>
<pre class="lang:c# decode:true">@using (Html.When(JqueryBind.Submit)
            .DoWithPreventDefault()
            .Submit(options =&gt;
                   {
                     options.Url = Url.Dispatcher()
                                     .Push(new AddTodoCommand { ClientId = Selector.Incoding.Cookie(CookieManager.ClientId) });
                   })
            .OnSuccess(dsl =&gt;
                           {
                               dsl.WithId(containerId).Core().Trigger.Incoding();
                               dsl.Self().Core().Form.Reset();
                           })
            .AsHtmlAttributes()
            .ToBeginTag(Html, HtmlTag.Form))
{
    @Html.TextBoxFor(r =&gt; r.Title, new { placeholder = "What needs to be done?", autofocus = "" })
}</pre>
<em>note: Anticipating phrases like “Hey, this is not serious, codes are much bigger, you need to copy everywhere, look at other people do it!!!” I’ve an argument – there are “ C# extensions ” which permit to wrap IML constructions. Further in the article I’ll give alternative variants of tasks decision (also “repository” on GibHub  with a revised <a href="https://github.com/IncodingSoftware/inc-todo-refactor">code</a>) using C# extensions</em>
<h6>What is what?</h6>
<ul>
	<li><strong>When(JqueryBind.Submit)</strong> - indicate objective event</li>
	<li><strong>DoWithPreventDefault - </strong> event behavior (cancel browser evaluator)</li>
	<li><strong>Submit</strong> - – send the form through ajax</li>
</ul>
<em>note:  few comments on presented realization:</em>
<ul>
<ul>
	<li><strong>Url</strong> where to send the form is set in options ( not through attribute variable “action” at "form"</li>
	<li><strong>ClientId </strong> can be derived as Hidden that on InitIncoding entered meaning from Cookies to call Submit with no parameters</li>
</ul>
	<li><strong>OnSuccess - </strong>realize after successful <strong>submit </strong>finish
<ul>
	<li><strong>Trigger Incoding to containerId</strong>  - start the whole <strong>IML</strong> code for Container element (description below)</li>
</ul>
</li>
</ul>
<em>note: more than one “When” may be used that allows to contact with different events (with different IML code), because trigger Incoding starts all chains.</em>
<ul>
<ul>
	<li><strong>Form reset</strong>  - make a reset of form elements</li>
</ul>
	<li><strong>AsHtmlAttributes</strong> - collect IML code in convenient for asp.net mvc (<strong>RouteValueDictionary</strong>)</li>
</ul>
<ul>
	<li><strong>ToBeginTag</strong> - – pack up getting attribute in the tag “form” ( work principle as <strong>Html.BeginForm</strong>)</li>
</ul>
<em>note: Html.BeginForm may be used (“action”,”controller”,Post,iml.AsHtmlAttributes())</em>
<h6>The form for TODO adding ( alternative variant )</h6>
<pre class="lang:c# decode:true">@using (Html.Todo().BeginForm(setting =&gt;
                             {
                               setting.TargetId = containerId;
                               setting.Routes = new { ClientId = Selector.Incoding.Cookie(CookieManager.ClientId) };
                             }))
{
    @Html.TextBoxFor(r =&gt; r.Title, new { placeholder = "What needs to be done?", autofocus = "" })
}</pre>
<em>note: code became smaller and the most important thing now you can expand method for concreate project needs (validation, redirect after submit and etc. )</em>

[wpspoiler name="Under hood" ]
<pre class="lang:c# decode:true">public class BeginFormSetting
{
    public string TargetId { get; set; }

    public object Routes { get; set; }
}

public BeginTag BeginForm(Action configure)
{
    var setting = new BeginFormSetting();
    configure(setting);

    var url = new UrlHelper(HttpContext.Current.Request.RequestContext);
    return this.helper.When(JqueryBind.Submit)
               .DoWithPreventDefault()
               .Submit(options =&gt;
                           {
                               options.Url = url.Dispatcher()
                                                .Push(setting.Routes);
                           })
               .OnSuccess(dsl =&gt;
                              {
                                  dsl.WithId(setting.TargetId).Core().Trigger.Incoding();
                                  dsl.Self().Core().Form.Reset();
                              })
               .AsHtmlAttributes()
               .ToBeginTag(this.helper, HtmlTag.Form);
}</pre>
<em>Note: Most of you are acquainted with asp.net mvc, but it is necessary to point out that in place of “usual” parameters, we propose anonymous method, that gets setting class.</em>
[/wpspoiler]
<h4>Container</h4>
<pre class="lang:c# decode:true">@(Html.When(JqueryBind.InitIncoding | JqueryBind.IncChangeUrl)
      .Do()
      .AjaxGet(Url.Dispatcher()
                  .Query(new
                         {
                                 ClientId = Selector.Incoding.Cookie(CookieManager.ClientId),
                                 Type = Selector.Incoding.HashQueryString(r =&gt; r.Type)
                         })
                  .AsJson())
      .OnSuccess(dsl =&gt;
                     {
                         string urlTmpl = Url.Dispatcher()
                                             .Model(new GetTodoByClientQuery.Tmpl { FooterId = footerId })
                                             .AsView("~/Views/Home/Todo_List_Tmpl.cshtml");
                         dsl.Self().Core().Insert.WithTemplateByUrl(urlTmpl).Html();
                         dsl.WithId(footerId).Core().Trigger.Incoding();
                     })
      .AsHtmlAttributes(new { id = containerId })
      .ToDiv())</pre>
<h6>WHAT’S WHAT?</h6>
<ul>
	<li><strong>When(JqueryBind.InitIncoding | IncChangeUrl)</strong> - – specifies target events
<ul>
	<li><strong>InitIncoding</strong> - – works at first appearance of element on a page (doesn’t matter ajax or usually)</li>
	<li><strong>IncChangeUrl</strong> - – works at “hash” changing</li>
</ul>
</li>
	<li><strong>Do - </strong>behavior of event</li>
	<li><strong>AjaxGet - </strong>indicate the url, which will be send to ajax request
<ul>
	<li><strong>ClientId</strong> - get the value from “cookies”</li>
	<li><strong>Type</strong> - get the value from “Hash Query String”</li>
</ul>
</li>
	<li><strong>OnSuccess</strong> - realize after successful AjaxGet finish
<ul>
	<li><strong>Insert data to self by template</strong> - put in findings from request ( json ) through template ( Todo_List_Tmpl below) in current element.</li>
</ul>
</li>
</ul>
<em>Note: template can be got through any available Selector, ex. earlier Jquery.Id was basic but uploading on ajax is preferable </em>
<ul>
<ul>
	<li><strong>Trigger incoding to footerId</strong> -  start entire IML code for footer element (description below )</li>
</ul>
	<li><strong>AsHtmlAttributes</strong> -collect IML code and set value containerId ( guid ) to Id attribute</li>
</ul>
<em>note: the use of guid as Id vouch for unique of element of page, especially relevant for single page application</em>
<ul>
	<li><strong>ToDiv - </strong>pack up getting attributes in the tag div</li>
</ul>
<em>note: ToDiv is C# extensions over RouteValueDictionary, that’s why it is easy to write necessary variant </em>
<h5>Container ( alternative way )</h5>
<pre class="lang:c# decode:true">@Html.Todo().Container(setting =&gt;
             {
                 setting.Id = containerId;
                 setting.Url = Url.Dispatcher()
                                  .Query(new
                                                {
                                                        ClientId = Selector.Incoding.Cookie(CookieManager.ClientId),
                                                        Type = Selector.Incoding.HashQueryString(r =&gt; r.Type)
                                                })
                                  .AsJson();
                 setting.Tmpl = Url.Dispatcher()
                                   .Model(new GetTodoByClientQuery.Tmpl { FooterId = footerId })
                                   .AsView("~/Views/Home/Todo_List_Tmpl.cshtml");
                 setting.DependencyId = footerId;
             })</pre>
<em>Note: in the future it’s necessary to add block ui or other actions, now you can do it centralize </em>

[wpspoiler name="Under hood " ]
<pre class="lang:c# decode:true">public class ContainerSetting
{
    public string Id { get; set; }

    public string Url { get; set; }

    public string Tmpl { get; set; }

    public string DependencyId { get; set; }
}

public MvcHtmlString Container(Action configure)
{
    var setting = new ContainerSetting();
    configure(setting);

    return helper.When(JqueryBind.InitIncoding | JqueryBind.IncChangeUrl)
                 .Do()
                 .AjaxGet(setting.Url)
                 .OnSuccess(dsl =&gt;
                                {
                                    dsl.Self().Core().Insert.WithTemplateByUrl(setting.Tmpl).Html();
                                    dsl.WithId(setting.DependencyId).Core().Trigger.Incoding();
                                })
                 .AsHtmlAttributes(new { id = setting.Id })
                 .ToDiv();
}</pre>
[/wpspoiler]
<h4>Footer</h4>
<pre class="lang:c# decode:true">@(Html.When(JqueryBind.None)
      .Do()
      .Direct(new FooterVm
                        {
                         AllCount = Selector.Jquery.Class("toggle").Length(),
                         IsCompleted = Selector.Jquery.Class("toggle").Is(JqueryExpression.Checked),
                         CompletedCount = Selector.Jquery.Class("toggle")
                                                         .Expression(JqueryExpression.Checked)
                                                         .Length(),
                        }))
      .OnSuccess(dsl =&gt;
                     {
                         string urlTmpl = Url.Dispatcher()
                                             .Model(new TodoFooterTmplVm
                                                        {
                                                                ContainerId = containerId
                                                        })
                                             .AsView("~/Views/Home/Todo_Footer_Tmpl.cshtml");
                         dsl.Self().Core().Insert.Prepare().WithTemplateByUrl(urlTmpl).Html();
                     })
      .AsHtmlAttributes(new { id = footerId })
      .ToDiv())</pre>
<h6><span style="font-size: 0.83em;">What’s what?</span></h6>
<ul>
	<li><strong>When(JqueryBind.None)</strong> - indicate target events
<ul>
	<li><b>None - </b>When allows to indicate any user’s event as a line “MySpecialEvent”, but experience has shown that for manu scripts one is enough.</li>
</ul>
</li>
	<li><strong>Do - </strong>behavior of event</li>
	<li><strong>Direct - </strong>can be examined as bib of action that perform no actions, but can work with data
<ul>
	<li><b>AllCount</b> - get the number of objects with “toggle” class</li>
</ul>
</li>
</ul>
<em>noteе: enhancing Method ( in place of Length ) could be used to call any jquery method and to write C# extensions over JquerySelectorExtend </em>
<ul>
<ul>
	<li><strong>IsCompleted</strong>  - check up for tagged objects with “toggle” class</li>
</ul>
</ul>
<em>note: if opportunities of ready-made jquery selector are not enough, so you can use Selector.Jquery.Custom(“your jquery selector”)</em>
<ul>
<ul>
	<li><strong>CompletedCount</strong> - get the numer of tagged objects with “toggle” class</li>
</ul>
</ul>
<em><em>note: to get the JS function value:</em>
</em>
<pre class="lang:c# decode:true">Selector.JS.Call("MyFunc",new []
                              {
                                      Selector.Jquery.Self(),
                                      "Arg2"                 
                              })</pre>
<ul>
	<li><strong>OnSuccess</strong> - realize after successful finish of AjaxGet
<ul>
	<li><strong>Insert prepare data to self by template </strong> - put in prepared data ( prepare ) from Direct through template (Todo_Footer_Tmpl below ) in the current element</li>
</ul>
</li>
</ul>
<em>note: before to put data in “prepare” fulfil selectors, that are in the fields. </em>
<ul>
	<li><strong>AsHtmlAttributes</strong> - collect IML code</li>
	<li><strong>ToDiv - </strong>pack up getting attributes in the tag div</li>
</ul>
<h3>Todo List Tmpl</h3>
Template markup for todo list building
<pre class="lang:c# decode:true">@using (var template = Html.Incoding().Template())
{
 &lt;ul&gt;
  @using (var each = template.ForEach()) 
   {
    @using (each.Is(r =&gt; r.Active)) 
    { @createCheckBox(true) }
    @using (each.Not(r =&gt; r.Active))
    { @createCheckBox(false) }

    &lt;li class="@each.IsInline(r=&gt;r.Active,"completed")"&gt;
      &lt;label&gt;@each.For(r=&gt;r.Title)&lt;/label&gt;
    &lt;/li&gt;
&lt;/ul&gt;
}</pre>
<em>Note: back code is more than shown in the example (logic of elements is deleted). It’s made for comfortable explanation of template </em>
<h6>What is what?</h6>
<ul>
	<li><strong>Html.Incoding().Template()</strong> -  open the context ( in the context of using) template building</li>
	<li><strong>template.ForEach() - </strong>start to iterate through elements ( in the context of using)</li>
	<li><strong>using(each.Is(r=&gt;r.Active)) - </strong>start to iterate through elements ( in the context of using)</li>
	<li><strong>createCheckBox - </strong>anonymous function C# функция for making checkbox (description below)</li>
	<li><strong>each.IsInline(r=&gt;r.Active,"completed") - </strong>if after Active true so get back “completed”</li>
</ul>
<em>note: there are also IsNotLine and IsLine. </em>
<ul>
	<li><strong>each.For(r =&gt; r.Title) - </strong>depict the value of Title field</li>
</ul>
<em>note: all accessing to fields happens on the base indicated model (Yep, I’m again about typification) </em>
<h3></h3>
<h3>Other elements</h3>
<h3><span style="font-size: 1em;">Button del</span></h3>
<pre class="lang:c# decode:true">@(Html.When(JqueryBind.Click)
      .Do()
      .AjaxPost(Url.Dispatcher().Push(new DeleteEntityByIdCommand
                                 {
                                         Id = each.For(r =&gt; r.Id),
                                         AssemblyQualifiedName = typeof(Todo).AssemblyQualifiedName
                                 }))
      .OnBegin(r =&gt;
                   {
                       r.WithSelf(s =&gt; s.Closest(HtmlTag.Li)).Core().JQuery.Manipulation.Remove();
                       r.WithId(Model.FooterId).Core().Trigger.Incoding();
                       r.WithId(toggleAllId).Core().Trigger.None();
                   })
      .AsHtmlAttributes(new { @class = "destroy" })
      .ToButton(""))</pre>
<h6><span style="font-size: 0.83em;">What is what ?</span></h6>
<ul>
	<li><strong>When(JqueryBind.Click)</strong> - – indicate target event</li>
	<li><strong>Do - </strong>behavior of event</li>
	<li><strong>AjaxPost-</strong> indicate Url, which will be send to ajax request
<ul>
	<li><b>Id</b>- value from Todo</li>
	<li><strong>AssemblyQualifiedName - </strong>get the name of element type ( or another C# code )</li>
</ul>
</li>
	<li><b>OnBegin</b>- take before the beginning of action (AjaxPost)</li>
</ul>
<em>Note: of course it’s better to use OnSuccess, because may happen a mistake on the server( timeoutor something else ) and transaction will not complete because OnBegin works before a call of AjaxPost, but the TodoMVC examples on js framework use local storage ( which is faster than ajax) and that’s why I’ve dodged in order not to lose operation speed. </em>
<ul>
<ul>
	<li><strong>Remove closest LI  </strong> - delete the closest LI</li>
	<li><strong>Trigger incoding to footer id</strong> - start the whole IML code for element footer (description above)</li>
	<li><strong>Trigger none to toggle all</strong> - start IML code (only None chain) for element Toggle All (description below)</li>
</ul>
</ul>
<em>Note: if for both to call the same trigger, so it would be possible to use the following variant </em>
<pre class="lang:c# decode:true">dsl.WithId(Model.FooterId, toggleAllId).Core().Trigger.Incoding();</pre>
<ul>
	<li><strong>AsHtmlAttributes</strong> - collect IML code</li>
	<li><strong>ToButton- </strong>pack up getting attribute in tag button</li>
</ul>
<em>Note: ToButton allows to indicate contents, but in this case it’s not necessary because the picture installs through CSS</em>
<h5>Button Del ( alternative variant )</h5>
<pre class="lang:c# decode:true">@Html.Todo().Verb(setting =&gt;
       {
           setting.Url = Url.Dispatcher().Push(new DeleteEntityByIdCommand
                                                   {
                                                           Id = each.For(r =&gt; r.Id),
                                                           AssemblyQualifiedName = typeof(Todo).AssemblyQualifiedName
                                                   });
           setting.OnBegin = dsl =&gt;
                                 {
                                     dsl.WithSelf(s =&gt; s.Closest(HtmlTag.Li)).Core().JQuery.Manipulation.Remove();
                                     dsl.WithId(Model.FooterId).Core().Trigger.Incoding();
                                     dsl.WithId(toggleAllId).Core().Trigger.None();
                                 };
           setting.Attr = new { @class = "destroy" };
       })</pre>
<em>Note: OnBegin take Action, that allows to scale easily your “extensions” instill in it IML. ( more examples further ) </em>

&nbsp;

[wpspoiler name="Under hood" ]
<pre class="lang:c# decode:true">public class VerbSetting
{
    public string Url { get; set; }

    public Action&lt;IIncodingMetaLanguageCallbackBodyDsl&gt; OnBegin { get; set; }

    public Action&lt;IIncodingMetaLanguageCallbackBodyDsl&gt; OnSuccess { get; set; }

    public object Attr { get; set; }

    public string Content { get; set; }
}

public MvcHtmlString Verb(Action&lt;VerbSetting&gt; configure)
{
    var setting = new VerbSetting();
    configure(setting);

    return this.helper.When(JqueryBind.Click)
               .Do()
               .AjaxPost(setting.Url)
               .OnBegin(dsl =&gt;
                            {
                                if (setting.OnBegin != null)
                                    setting.OnBegin(dsl);
                            })
               .OnSuccess(dsl =&gt;
                              {
                                  if (setting.OnSuccess != null)
                                      setting.OnSuccess(dsl);
                              })
               .AsHtmlAttributes(setting.Attr)
               .ToButton(setting.Content);
}</pre>
<em>note: as long Verb uses in some scripts, it'’ easy to make optional parameters. I check them on “null” and assign a value on default</em>

[/wpspoiler]
<h4> Checkbox Completed</h4>
<pre class="lang:c# decode:true">var createCheckBox = isValue =&gt; Html.When(JqueryBind.Change)
                                    .Do()
                                    .AjaxPost(Url.Dispatcher().Push(new ToggleTodoCommand
                                                                 {
                                                                     Id = each.For(r =&gt; r.Id)
                                                                 }))
                                    .OnBegin(dsl =&gt;
                                                  {
                                                   dsl.WithSelf(r =&gt; r.Closest(HtmlTag.Li))
                                                      .Behaviors(inDsl =&gt;
                                                                     {
                                                                 inDsl.Core().JQuery.Attributes.RemoveClass("completed");
                                                                 inDsl.Core().JQuery.Attributes.AddClass("completed")
                                                                                               .If(builder =&gt; builder.Is(() =&gt; Selector.Jquery.Self()));
                                                                     });

                                                    dsl.WithId(Model.FooterId).Core().Trigger.Incoding();
                                                    dsl.WithId(toggleAllId).Core().Trigger.None();
                                                   })
                                     .AsHtmlAttributes(new {@class="toggle" })
                                     .ToCheckBox(isValue);</pre>
<em>note: as long Verb uses in some scripts, it'’ easy to make optional parameters. I check them on “null” and assign a value on default</em>
<h6><span style="font-size: 0.83em;">What is what ?</span></h6>
<ul>
	<li><strong>When(JqueryBind.Change)</strong> - indicate target event</li>
	<li><strong>Do - </strong>– behavior of event</li>
	<li><strong>AjaxPost -</strong> indicate Url, which will be send to ajax request</li>
</ul>
<em>Note: AjaxPost and AjaxGet is “denominate“ version of Ajax, which has many additional tuning OnBegin – take before the start actions (AjaxPost)</em>
<ul>
	<li><strong>OnBegin - </strong>take before the start actions (AjaxPost)
<ul>
	<li><strong>Remove class on closest LI</strong> -  delete class “completed” at the nearest LI</li>
	<li><strong>Add class on closest LI  if self is true</strong>- add class “completed”</li>
</ul>
</li>
</ul>
<em>Note: now in IML doesn’t realized a possibility “else” , but in 2.0 version is planted </em>
<ul>
	<li><strong>AsHtmlAttributes</strong> - collect IML code and install the value “toggle” to attribute “class”</li>
	<li><strong>ToCheckBox - </strong>pack up</li>
</ul>
<h4>Filter by type todo</h4>
<pre class="lang:c# decode:true">@{
    const string classSelected = "selected";
    var createLi = (typeOfTodo,isFirst) =&gt; Html.When(JqueryBind.InitIncoding)
                                               .Do()
                                               .Direct()
                                               .OnSuccess(dsl =&gt;
                                                           {
                                                             var type = Selector.Incoding.HashQueryString(r =&gt; r.Type);
                                                              if (isFirst)
                                                             dsl.Self().Core().JQuery.Attributes.AddClass(classSelected).If(s =&gt; s.Is(() =&gt; type == ""));

                                                             dsl.Self().Core().JQuery.Attributes.AddClass(classSelected).If(s =&gt; s.Is(() =&gt; type == typeOfTodo.ToString()));
                                                              })
                                               .When(JqueryBind.Click)
                                               .Do()
                                               .Direct()
                                               .OnSuccess(dsl =&gt;
                                                           {
                                                              dsl.WithSelf(r =&gt; r.Closest(HtmlTag.Ul).Find(HtmlTag.A)).Core().JQuery.Attributes.RemoveClass(classSelected);
                                                              dsl.Self().Core().JQuery.Attributes.AddClass(classSelected);                                                   
                                                           })
                                               .AsHtmlAttributes(new { href = "#!".AppendToHashQueryString(new { Type = typeOfTodo }) })
                                               .ToLink(typeOfTodo.ToString());
}

 &lt;li&gt;  @createLi(GetTodoByClientQuery.TypeOfTodo.All,true)        &lt;/li&gt;  
 &lt;li&gt;  @createLi(GetTodoByClientQuery.TypeOfTodo.Active,false)    &lt;/li&gt;
 &lt;li&gt;  @createLi(GetTodoByClientQuery.TypeOfTodo.Completed,false) &lt;/li&gt;</pre>
<em>Note: one more example how to realize anonymous functions in the context of “razot view”</em>
<h6>What is what?</h6>
<ul>
	<li><strong>When(JqueryBind.InitIncoding)</strong> - indicate target event</li>
	<li><strong>Do - </strong>– behavior event</li>
	<li><strong>Direct - </strong>realize noting</li>
	<li><strong>OnSuccess</strong> - realize after successful finish</li>
</ul>
<em>Note: there is no differences between “OnBegin” and “OnSuccess” for”Direct”, but OnError and OnBreak works in the same way as for others. </em>
<ul>
<ul>
	<li><strong>var type</strong> - declare a variable that we will use in expressions.</li>
	<li><strong>add class to self if IsFirst is true And type is Empty</strong> - add class if current element is the first and in “type” is empty</li>
	<li><strong>add class to self if type is current type</strong> - add class to current element if “type” is equal to argument typeOfTodo</li>
</ul>
	<li><strong>When(JqueryBind.Click)</strong> - indicate target event</li>
	<li><strong>Do - </strong>behavior of event</li>
</ul>
<em>Note: we don’t cancel behavior of hyperlink because we need the browser to update location</em>
<ul>
	<li><strong>Direct - </strong>take noting
<ul>
	<li><strong>remove class</strong> - delete class selected at all A, that are in the nearest UL</li>
	<li><strong>add class to self - </strong>add class selected to the current element</li>
</ul>
</li>
	<li><strong>AsHtmlAttributes - </strong>collect IML code and install attribute “href”</li>
</ul>
<em> </em>
<h5>Filter by type todo ( alternative method )</h5>
<pre class="lang:c# decode:true">&lt;li&gt;
    @Html.Todo().LiHash(setting =&gt;
                            {
                         setting.IsFirst = true;
                         setting.SelectedClass = classSelected;
                          setting.Type = GetTodoByClientQuery.TypeOfTodo.All;
                            })                    
&lt;/li&gt;</pre>
&nbsp;

[wpspoiler name="Under hood" ]
<pre class="lang:c# decode:true">public class LiHashSetting
{            
    public bool IsFirst { get; set; }

    public string SelectedClass { get; set; }

    public GetTodoByClientQuery.TypeOfTodo Type { get; set; }
}

public MvcHtmlString LiHash(Action configure)
{
    var setting = new LiHashSetting();
    configure(setting);

    return helper.When(JqueryBind.InitIncoding)
                 .Do()
                 .Direct()
                 .OnSuccess(dsl =&gt;
                                {
                                    var type = Selector.Incoding.HashQueryString(r =&gt; r.Type);
                                    if (setting.IsFirst)
                                        dsl.Self().Core().JQuery.Attributes.AddClass(setting.SelectedClass).If(s =&gt; s.Is(() =&gt; type == ""));

                                    dsl.Self().Core().JQuery.Attributes.AddClass(setting.SelectedClass).If(s =&gt; s.Is(() =&gt; type == setting.Type.ToString()));
                                })
                 .When(JqueryBind.Click)
                 .Do()
                 .Direct()
                 .OnSuccess(dsl =&gt;
                                {
                                    dsl.WithSelf(r =&gt; r.Closest(HtmlTag.Ul).Find(HtmlTag.A)).Core().JQuery.Attributes.RemoveClass(setting.SelectedClass);
                                    dsl.Self().Core().JQuery.Attributes.AddClass(setting.SelectedClass);
                                })
                 .AsHtmlAttributes(new { href = "#!".AppendToHashQueryString(new { Type = setting.Type }) })
                 .ToLink(setting.Type.ToString());
}</pre>
[/wpspoiler]
<h3><span style="font-size: 1.17em;">Absolute advantages !</span></h3>
The advantages of IML I’ve tried to show in the last article, but it wasn’t convincing, that’s why I will try again:
<ul>
	<li><strong>Typification</strong> - of course, each looks at typification from its own point of view. Somebody think that hier you have to write more codes (that’s right), others doesn’t have enough flexibility that is inherent to non-typification languages, but IML is first of all C#, so those developers who chose it, I think, will appreciate this advantage</li>
	<li><strong>Powerful extensions </strong> - in the article I gave some of them, but there are much more in practice. I give some more examples to put the words into action:
<ul>
	<li><strong>Drop down</strong></li>
</ul>
</li>
</ul>
<pre class="lang:c# decode:true"> @Html.For(r=&gt;r.HcsId).DropDown(control =&gt;
                                {
                 control.Url = Url.Action("HealthCareSystems", "Shared");
                 control.OnInit = dsl =&gt; dsl.Self().Core().Rap().DropDown();
                 control.Attr(new { @class = "selectInput", style = "width:375px" });
                                })</pre>
<em>примечание: OnInit принимает Action&lt;IIncodingMetaLanguageCallbackDsl&gt;, что позволяет легко масштабировать ваш extensions внедряя в него IML. </em>
<ul>
<ul>
	<li><strong>Dialog</strong></li>
</ul>
</ul>
<pre class="lang:c# decode:true">@Html.ProjectName().OpenDialog(setting =&gt;
                         {
                            setting.Url = Url.Dispatcher()
                                             .Model&lt;GroupEditProviderOrderCommand&gt;()
                                             .AsView("~/Views/ProviderOrder/Edit.cshtml");
                            setting.Content = "Edit";
                            setting.Options = options =&gt; { options.Title = "Edit Order"; };
                         })</pre>
<em>note: OnInit takes Action&lt;JqueryUIDialogOptions&gt;, that allows to scale easily your extensions instill in it IML.  </em>

The list could be endless, but the main idea is that IML allows to perform any task, while html extensions solves a problem with design reuse.
<ul>
	<li><strong>Much more powerful extensions</strong>
<ul>
	<li><strong>Grid -</strong> build entirely on IML (documentation will be soon)</li>
</ul>
</li>
</ul>
<pre class="lang:c# decode:true">@(Html.ProjectName()
      .Grid&lt;CTRPrintLogModel&gt;()
      .Columns(dsl =&gt;
               {
                   dsl.Template(@&lt;text&gt;
                                    &lt;span&gt;@item.For(r=&gt;r.Comment)&lt;/span&gt;
                                 &lt;/text&gt;)           
                           .Title("Comment");

                   const string classVerticalTop = "vertical_top";
                   dsl.Bound(r =&gt; r.Time).Title("Time").HtmlAttributes(new { @class = classVerticalTop });                   
                   dsl.Bound(r =&gt; r.Version).Title("Type").HtmlAttributes(new { @class = classVerticalTop });                   
                   dsl.Bound(r =&gt; r.Staff.FirstAndLastName).Title("Staff").HtmlAttributes(new { @class = classVerticalTop });   
                   dsl.Bound(r =&gt; r.PrintDate).Title("Date");
                   dsl.Bound(r =&gt; r.Comment).Raw();
               })
      .AjaxGet(Url.RootAction("GetCTRPrintLogModel", "CTR")))</pre>
<ul>
<ul>
	<li> <strong>Tabs - </strong></li>
</ul>
</ul>
<pre class="lang:c# decode:true">@(Html.Rap()
      .Tabs&lt;Enums.CarePlanTabs&gt;()
      .Items(dsl =&gt;
             {
                 dsl.AddTab(Url.Action("GapsAndBarriersInc", "GapsAndBarriers"), Enums.CarePlanTabs.GapsAndBarriers);                   
                 dsl.AddTab(Url.Action("RedFlags", "PatientRedFlag"), Enums.CarePlanTabs.RedFlags);                             
                 dsl.AddTab(Url.Action("Goals", "IncGoal"), Enums.CarePlanTabs.SelfCareGoals);
                 dsl.AddTab(Url.Action("Index", "IncAppointment"), Enums.CarePlanTabs.Appointments);        
             }))</pre>
<em> Note: every developer who knows html extensions can build such element for his project needs </em>
<ul>
	<li><strong>• The Work with hash </strong> - in this article was examined only on IncChangeUrl level, but we have:
<ul>
	<li> <strong>Hash.Fetch - </strong>put values from hash into ( sandbox ) elements</li>
	<li> <strong>Hash.Insert/Update -</strong> – put values into hash from elements</li>
	<li> <strong>Hash.Manipulate</strong> - allows delicately ( set/ remove by key ) tune current hash</li>
	<li><strong>AjaxHash</strong> - is an analog of Submit, not for form, but for Hash</li>
</ul>
</li>
	<li><strong>The Work with Insert</strong> - – for TOSO realization I didn’t have to use, but in real projects it’s used everywhere
<ul>
	<li><strong>Insert Generic</strong>  - all the examples above were built on one model, but it’s often happens that scripts where findings are “containers”. In this case in Insert is an possibility to indicate with what part of model we work through For and also “template” for each its own</li>
</ul>
</li>
</ul>
<pre class="lang:c# decode:true">Html.When(JqueryBind.InitIncoding)
    .Do()
    .AjaxGet(Url.Action("FetchComplex", "Data"))
    .OnSuccess(dsl =&gt;
                   {
            dsl.WithId(newsContainerId).Core().Insert.For&lt;ComplexVm&gt;(r =&gt; r.News).WithTemplateByUrl(urlTemplateNews).Html();
            dsl.WithId(contactContainerId).Core().Insert.For&lt;ComplexVm&gt;(r =&gt; r.Contacts).WithTemplateByUrl(urlTemplateContacts).Html();
                   })
    .AsHtmlAttributes()
    .ToDiv()</pre>
<ul>
	<li><strong>The Work with validation (server as a client) </strong> - there are instrument for validation in may js framework, but IML has integration with server and backing any validation engine ( FluentValidation, standart MVC) with no necessary to write an additional code.
<ul>
	<li><strong><span style="font-size: 0.75em;">Код command</span></strong></li>
</ul>
</li>
</ul>
<pre class="lang:c# decode:true">if (device != null)
   throw IncWebException.For&lt;AddDeviceCommand&gt;(r =&gt; r.Pin, "Device with same pin is already exist");</pre>
<ul>
<ul>
	<li><span style="font-size: 0.75em;"> </span><strong><span style="font-size: 0.75em;">Код view</span></strong></li>
</ul>
</ul>
<pre class="lang:c# decode:true">.OnError(dsl =&gt; dsl.Self().Core().Form.Validation.Refresh())</pre>
<em> Note: handler OnError must be attached to the element that causes action ( submit , ajaxpost or etc ) </em>
<ul>
	<li><strong>Less scripts</strong> - with increase of project js framework demands lots of js files, but IML has fixed set of libraries (plug-ins not counted)</li>
	<li><strong>Typificated template </strong> - about typification by kind, but for template constructing it’s very important</li>
	<li><strong>template replace by engine - </strong>choose any of them, syntax is the same</li>
	<li><strong>Complex solution</strong> - IML is a part of Incoding Framework and in contrast to js framework we have full infrastructure (server/client/ unit testing ) for projects developing that is tightly integrated with each other</li>
</ul>
<h2>Conclusion</h2>
At realization of IML I’ve followed the rule: less updates to the page, I mean I recounted this all on the client, but in practice (of ours projects) often the weak part is server, because a lot of actions are impossible or preferable on client. For example

Impossible (because of productivity):
<ul>
	<li><strong>Paginated</strong> - of the base consists of hundreds of thousands of recordings, it’s wrong to deliver capacity on client</li>
	<li><strong>Order</strong> - the same reason</li>
	<li><strong>Where</strong> - the same reason</li>
	<li>And other actions that connects with the data base</li>
</ul>
Difficult calculation on the base of values of fields (total amount of order including tax) could be not preferable. It’s better send on server an request (with values of fields) and put in results

&nbsp;

[wpspoiler name="Again IML variant" ]

In the context of IML calculation could be solved by following ways:
<ul>
	<li><strong>Single value</strong></li>
</ul>
<pre class="lang:c# decode:true">var val = Selector.Incoding.AjaxGet(url);
dsl.WithId(yourId).Core().JQuery.Attributes.Val(val);</pre>
<ul>
	<li><strong>Data set</strong></li>
</ul>
<pre class="lang:c# decode:true">dsl.With(r =&gt; r.Name(s =&gt; s.Last)).Core().Insert.For&lt;ContactVm&gt;(r =&gt; r.Last).Val();
dsl.With(r =&gt; r.Name(s =&gt; s.First)).Core().Insert.For&lt;ContactVm&gt;(r =&gt; r.First).Val(); 
dsl.With(r =&gt; r.Name(s =&gt; s.City)).Core().Insert.For&lt;ContactVm&gt;(r =&gt; r.City).Val();</pre>
[/wpspoiler]

I can continue to tell you about IML possibilities (and Incoding Framework) but the article as already long. That’s why those who want to learn more about our tool can find the materials in the Net. I understand to prove that IML is able to solve tasks not worse than popular js framework is rather difficult, but in the next articles I will make a review of autocomplete, Tree View, grid and other popular scripts realization, that will show more possibilities of our toll.
