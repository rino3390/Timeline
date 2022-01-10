// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

function AutoHide() {
	return function () {
		el_autohide = document.querySelector('.autohide');

		// add padding-top to bady (if necessary)
		navbar_height = document.querySelector('.navbar').offsetHeight;
		document.body.style.paddingTop = navbar_height + 'px';

		if (el_autohide) {
			var last_scroll_top = 0;
			window.addEventListener('scroll', function () {
				let scroll_top = window.scrollY;
				if (scroll_top < last_scroll_top) {
					el_autohide.classList.remove('scrolled-down');
					el_autohide.classList.add('scrolled-up');
				} else {
					el_autohide.classList.remove('scrolled-up');
					el_autohide.classList.add('scrolled-down');
				}
				last_scroll_top = scroll_top;
			});
		}
	};
}

// Write your JavaScript code.
document.addEventListener("DOMContentLoaded", AutoHide());

(function () {
	"use strict";

	// define variables
	var items = document.querySelectorAll(".timeline li");

	// check if an element is in viewport
	// http://stackoverflow.com/questions/123999/how-to-tell-if-a-dom-element-is-visible-in-the-current-viewport
	function isElementInViewport(el) {
		var rect = el.getBoundingClientRect();
		return (
			rect.top >= 0 &&
			rect.left >= 0 &&
			rect.bottom <=
			(window.innerHeight || document.documentElement.clientHeight) &&
			rect.right <= (window.innerWidth || document.documentElement.clientWidth)
		);
	}

	function callbackFunc() {
		for (var i = 0; i < items.length; i++) {
			if (isElementInViewport(items[i])) {
				items[i].classList.add("in-view");
			}
		}
	}
	
	function setBlankTimelin(){
		document.getElementById("timeline").style.height = (screen.height * 0.60).toString()+"px";
	}

	// listen for events
	window.addEventListener("load", callbackFunc);
	window.addEventListener("load", setBlankTimelin);
	window.addEventListener("resize", callbackFunc);
	window.addEventListener("scroll", callbackFunc);
})();