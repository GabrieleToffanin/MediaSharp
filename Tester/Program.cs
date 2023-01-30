using MediaSharp.Core;
using MediaSharp.Core.Internal;


var cheneso = new Bho(1);
var handler = new BhoHandler(new MediatorContext());

var mediator = new Mediator();

var result = mediator.SendAsync(cheneso, CancellationToken.None);