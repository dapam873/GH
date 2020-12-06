var miniImg = function()
{
    var clone = this.cloneNode();
    clone.classList.remove("mini");
    var lb = document.getElementById("modal-image");
    lb.innerHTML = "";
    lb.appendChild(clone);
    lb = document.getElementById("modal-arriere");
    lb.classList.add("show");
};
window.addEventListener("load", function()
{
    var images = document.getElementsByClassName("mini");
    if (images.length > 0)
    {
        for (var img of images)
        {
            img.addEventListener("click", miniImg);
        }
    }
    document.getElementById("modal-arriere").addEventListener("click", function()
    {
       this.classList.remove("show");
    })
});
