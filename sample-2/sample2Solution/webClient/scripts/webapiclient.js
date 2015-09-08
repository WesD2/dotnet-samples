/// <reference path="./references.js" />

(function () {

	String.prototype.format = function (dataObject) {
		//return this.replace(/{(.+)}/g, function (match, propertyName) {
		//	return dataObject[propertyName];
		//});
		return dataObject;
	};

	function configureAjax() {

		$.ajax({
			success: function (data, status, jqxhr) {
				handleResponse(status, data, null, jqxhr);
			},
			error: function (jqxhr, status, errorMsg) {
				handleResponse(status, null, errorMsg, jqxhr);
			},
			beforeSend: function (jqxhr, settings) {
				settings.url = "mydata.json";
			}
		});

	}



	function displayData(data) {
		var target = $("#dataTable tbody");
		target.empty();
		var template = $("#rowTemplate");
	//	data.forEach(function (dataObject) {
		//		data.pangrams.forEach(function (dataObject) {
		for (i=0; i < data.pangrams.length; i++)
		{
		//	target.append(template.html().format(dataObject + '\n'));
			target.append(template.html().format('<tr><td>' + data.pangrams[i] + '</td></tr>'));
		}
//		});
		$(target).find("button").click(function (e) {
			$("*.errorMsg").remove();
			$("*.error").removeClass("error");
			var index = $(e.target).attr("data-id");
			if ($(e.target).attr("data-action") == "delete") {
				deleteData(index);
			} else {
				var pangrams = { sentence: index };
				$(e.target).closest('tr').find('input')
						.each(function (index, inputElem) {
							pangrams[inputElem.name] = inputElem.value;
						});
				updateData(index, productData);
			}
			e.preventDefault();
		});
	}

	function getData() {

		$.getJSON("/api/pangrams", null, displayData);

	};



	function postData(e) {
		var txt = $('#pangramText').val();

		$.ajax({
			url: $('form').attr("action"),
			type: 'POST',
			contentType: 'application/x-www-form-urlencoded; charset=utf-8',
			data: '=' + encodeURIComponent(txt),
			success: processPostResult
		})
		e.preventDefault();
	}

	function processPostResult(data) {

		if (data.isPangram == false) {
			alert("not a pangram");
		}
		if (data.isPangram == true) {
			alert("this is a valid pangram. If it was not already in the pangram repository it has been added.")
		}



	};

	function deleteData(index) {
		$.ajax({
			url: "/api/product/" + index,
			type: 'DELETE',
			success: getData
		});
	}

	function updateData(index, productData) {
		$.ajax({
			url: "/api/product/" + index,
			type: 'PUT',
			data: productData,
			success: getData,
			error: function (jqXHR, status, error) {
				var errorRow = $("button[data-id=" + index + "]").closest("tr");
				errorRow.find("*").addClass("error");
				var errData = JSON.parse(jqXHR.responseText);
				for (var i = 0; i < errData.length; i++) {
					errorRow.after("<tr class='errMsg error'><td/><td colspan=3>"
							+ errData[i] + "</td></tr>");
				}
			}
		});
	}






	var EventUtil = new Object;
	EventUtil.addEventHandler = function (oTarget, sEventType, fnHandler) {
		if (oTarget.addEventListener) {
			oTarget.addEventListener(sEventType, fnHandler, false);
		} else if (oTarget.attachEvent) {
			oTarget.attachEvent("on" + sEventType, fnHandler);
		} else {
			oTarget["on" + sEventType] = fnHandler;
		}
	};

	EventUtil.removeEventHandler = function (oTarget, sEventType, fnHandler) {
		if (oTarget.removeEventListener) {
			oTarget.removeEventListener(sEventType, fnHandler, false);
		} else if (oTarget.detachEvent) {
			oTarget.detachEvent("on" + sEventType, fnHandler);
		} else {
			oTarget["on" + sEventType] = null;
		}
	};

	EventUtil.formatEvent = function (oEvent) {

		if (typeof oEvent.charCode == "undefined") {
			oEvent.charCode = (oEvent.type == "keypress") ? oEvent.keyCode : 0;
			oEvent.isChar = (oEvent.charCode > 0);
		}

		if (oEvent.srcElement && !oEvent.target) {
			oEvent.eventPhase = 2;
			oEvent.pageX = oEvent.clientX + document.body.scrollLeft;
			oEvent.pageY = oEvent.clientY + document.body.scrollTop;

			if (!oEvent.preventDefault) {
				oEvent.preventDefault = function () {
					this.returnValue = false;
				};
			}

			if (oEvent.type == "mouseout") {
				oEvent.relatedTarget = oEvent.toElement;
			} else if (oEvent.type == "mouseover") {
				oEvent.relatedTarget = oEvent.fromElement;
			}

			if (!oEvent.stopPropagation) {
				oEvent.stopPropagation = function () {
					this.cancelBubble = true;
				};
			}

			oEvent.target = oEvent.srcElement;
			oEvent.time = (new Date).getTime();

		}

		return oEvent;
	};

	EventUtil.getEvent = function () {
		if (window.event) {
			return this.formatEvent(window.event);
		} else {
			return EventUtil.getEvent.caller.arguments[0];
		}
	};




	function setupButton() {
		// Get a reference to the list element surrounding all the links we wish to
		// assign the event handler to
		var list = document.getElementById("table1");

		function onclick(evt) {

			var clickedElem = evt.target,
				tagNameSought = "BUTTON";

			if (clickedElem && clickedElem.tagName === tagNameSought) {


				postData(evt);

			}
		}

		// Assign the event handler to the list item, the parent surrounding all the links. Adding
		// one event handler is faster than assigning an event handler to each of the individual
		// links. The third parameter is set to ‘false’, indicating that events should be handled
		// in the bubble phase of the event lifecycle, from the element the event occurred on, up the
		// tree to the list item itself
	//	list.addEventListener("click", onclick, false);
		EventUtil.addEventHandler(list, "click", onclick);

	}

	function setupButtonjq() {



		$('pangramButton').click(function (e) {

//		var jsonstring = $('pangramText').text;
			//				jsonstring = jsonstring.replace(/&quot;/g, '"');
		//	var obj = JSON.parse('{ "Sentence":"' + jsonstring + '"}');

		//	var customer = { contact_name: "Scott", company_name: "HP" };
//			var obj = { Sentence: jsonstring  };

			$.ajax({
				url: "/api/pangramstext",
	//			data: $('form').serialize(),
	//			data: $('pangramText').text, //.serialize(),
	//			data: JSON.stringify(obj), //.serialize(),
				data : JSON.stringify({ 'sentence': 'hello' }),
				type: 'POST',
		//		contentType: "application/json",
				//contentType: "application/x-www-form-urlencoded",
				//dataType: 'json',
				contentType: 'application/bson; charset=utf-8',
				dataType: 'json',
				success: getData
			})
			e.preventDefault();

		});

	}






	$(document).ready(function () {
			setupButton();
		getData();
	});

})();
