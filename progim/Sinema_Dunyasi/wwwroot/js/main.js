// ----- swiper initializing -----
var swiper = new Swiper(".newSwiper", {
    loop: true,
    centeredSlides: false,
    slidesPerView: 1,
    spaceBetween: 5,

    autoplay: {
        delay: 2500,
        disableOnInteraction: false,
    },

    navigation: {
        nextEl: ".pw-new-next",
        prevEl: ".pw-new-prev",
    },
    breakpoints: {
        768: {
            slidesPerView: 2,
            spaceBetween: 50,
            centeredSlides: false,
        },
        1200: {
            slidesPerView: 3,
            spaceBetween: 70,
            centeredSlides: false,
        },
    },
});

// -------- şifre açma ve gizleme --------
const eyes = document.querySelectorAll(".fa-eye");

eyes.forEach((eyeIcon) => {
    eyeIcon.addEventListener("click", function () {
        const inputField =
            this.closest(".position-relative").querySelector("input");
        if (inputField.type === "password") {
            inputField.type = "text";
        } else {
            inputField.type = "password";
        }
    });
});

// ----- kategori ekleme -----
const uploadContainer1 = document.getElementById("upload-container1");
const fileInput1 = document.getElementById("file-input1");

uploadContainer1.addEventListener("click", () => {
    fileInput1.click();
});

fileInput1.addEventListener("change", (event) => {
    const file1 = event.target.files[0];
    if (file1) {
        const reader1 = new FileReader();
        reader1.onload = (e) => {
            uploadContainer1.innerHTML = '<img src="${e.target.result}" alt="Uploaded Image">';
        };
        reader1.readAsDataURL(file1);
    }
});

// ----- kategori ekleme başarılı ------
const kategoriModal = document.querySelector("#exampleModal1");
const basariKategoriModal = document.querySelector("#exampleModal11");
const bosAdErr = document.querySelector(".bos-ad-err");
const resimErr = document.querySelector(".resim-error");
const adInpt = document.querySelector(".ad-input");
const formKategori = document.getElementById("kategoriForm");
const fileInputt = document.querySelector("#file-input1");

formKategori.addEventListener("submit", function (e) {
    e.preventDefault();

    let isValid = true;

    if (adInpt.value.trim() === "") {
        bosAdErr.classList.replace("d-none", "d-block");
        isValid = false;
    } else {
        bosAdErr.classList.replace("d-block", "d-none");
    }

    if (fileInputt.files.length === 0) {
        resimErr.classList.replace("d-none", "d-block");
        isValid = false;
    } else {
        resimErr.classList.replace("d-block", "d-none");
    }

    if (isValid) {
        const bootstrapKategoriModal = bootstrap.Modal.getInstance(kategoriModal);
        bootstrapKategoriModal.hide();

        const bootstrapBasariModal = new bootstrap.Modal(basariKategoriModal);
        bootstrapBasariModal.show();

        setTimeout(() => {
            bootstrapBasariModal.hide();

            formKategori.reset();
            uploadContainer1.innerHTML = '<span class="plus-icon">+</span>'; // Görsel içeriği sıfırlama
            fileInputt.value = ""; // Dosya inputunu sıfırlayın
        }, 3000);
    }
});

kategoriModal.addEventListener("hidden.bs.modal", function () {
    formKategori.reset();
    uploadContainer1.innerHTML = '<span class="plus-icon">+</span>';
    bosAdErr.classList.add("d-none");
    resimErr.classList.add("d-none");
    fileInputt.value = "";
});

// ----- kitap ekleme -----
const uploadContainer2 = document.getElementById("upload-container2");
const fileInput2 = document.getElementById("file-input2");

uploadContainer2.addEventListener("click", () => {
    fileInput2.click();
});

fileInput2.addEventListener("change", (event) => {
    const file2 = event.target.files[0];
    if (file2) {
        const reader2 = new FileReader();
        reader2.onload = (e) => {
            uploadContainer2.innerHTML = '<img src="${e.target.result}" alt="Uploaded Image">';
        };
        reader2.readAsDataURL(file2);
    }
});

//----- Kitap ekleme başarılı -----
const kitapForm = document.getElementById("kitapForm");
const kitapAdInput = document.getElementById("kitapAdInput");
const kategoriDropdown = document.getElementById("kategoriDropdown");
const aciklamaTextarea = document.getElementById("aciklamaTextarea");
const aciklamaErr = document.querySelector(".aciklamaErr");
const kitapAdErr = document.querySelector(".kitapAdErr");
const kategoriAdError = document.querySelector(".kategoriAdError");
const resimError = document.querySelector(".resimError"); // Resim hata mesajı

let selectedKategori = null;

kategoriDropdown.querySelectorAll(".dropdown-item").forEach((item) => {
    item.addEventListener("click", function () {
        selectedKategori = item.textContent.trim();
        kategoriDropdown.querySelector(".text-start").textContent = selectedKategori;
        kategoriAdError.classList.add("d-none");
    });
});

kitapForm.addEventListener("submit", (e) => {
    e.preventDefault();

    let isValid = true;

    if (kitapAdInput.value.trim() === "") {
        kitapAdErr.classList.remove("d-none");
        isValid = false;
    } else {
        kitapAdErr.classList.add("d-none");
    }

    if (!selectedKategori) {
        kategoriAdError.classList.remove("d-none");
        isValid = false;
    } else {
        kategoriAdError.classList.add("d-none");
    }

    if (aciklamaTextarea.value.trim() === "") {
        aciklamaErr.classList.remove("d-none");
        isValid = false;
    } else {
        aciklamaErr.classList.add("d-none");
    }

    if (fileInput2.files.length === 0) {
        resimError.classList.remove("d-none"); // Resim alanı kontrolü ve hata mesajı
        isValid = false;
    } else {
        resimError.classList.add("d-none");
    }

    if (isValid) {
        const bootstrapKitapModal = bootstrap.Modal.getInstance(
            document.querySelector("#exampleModal2")
        );
        bootstrapKitapModal.hide();

        const bootstrapBasariModal = new bootstrap.Modal(
            document.querySelector("#exampleModal22")
        );
        bootstrapBasariModal.show();

        setTimeout(() => bootstrapBasariModal.hide(), 3000);

        // Formu sıfırla
        kitapForm.reset();
        selectedKategori = null;
        kategoriDropdown.querySelector(".text-start").textContent = "Kategori Seç";
        uploadContainer2.innerHTML = '<span class="plus-icon">+</span>'; // Resim içeriğini sıfırla
    }
});

document.querySelector("#exampleModal2").addEventListener("hidden.bs.modal", function () {
    kitapForm.reset();
    selectedKategori = null;
    kategoriDropdown.querySelector(".text-start").textContent = "Kategori Seç";
    uploadContainer2.innerHTML = '<span class="plus-icon">+</span>'; // Resim içeriğini sıfırla
    kitapAdErr.classList.add("d-none");
    kategoriAdError.classList.add("d-none");
    aciklamaErr.classList.add("d-none");
    resimError.classList.add("d-none"); // Resim hata mesajını sıfırla
});


// ----- kategori düzeltme başarılı ------
const uploadContainer4 = document.getElementById("upload-container4");
const fileInput4 = document.getElementById("file-input4");

uploadContainer4?.addEventListener("click", () => {
    fileInput4.click();
});

fileInput4?.addEventListener("change", (event) => {
    const file4 = event.target.files[0];
    if (file4) {
        const reader4 = new FileReader();
        reader4.onload = (e) => {
            uploadContainer4.innerHTML = '<img src="${e.target.result}" alt="Uploaded Image" style="max-width: 100%;">';
        };
        reader4.readAsDataURL(file4);
    }
});

const btnDuzelt = document.getElementById("btnDuzelt");
const duzeltmeModal = new bootstrap.Modal(
    document.getElementById("exampleModal3")
);
const basariModal = new bootstrap.Modal(
    document.getElementById("exampleModal33")
);

btnDuzelt.addEventListener("click", function () {
    duzeltmeModal.hide();

    basariModal.show();

    setTimeout(() => {
        basariModal.hide();
    }, 3000);
});

// ----- iletişim validation -----
const iletisimForm = document.querySelector(".iletisim-form");
const adSoyadInput = document.querySelector(".ad-iletisim-input");
const epostaInput = document.querySelector(".eposta-iletisim-input");
const mesajTextarea = document.querySelector(".mesaj-textarea");

const adSoyadError = document.querySelector(".ad-iletisim-err");
const epostaError = document.querySelector(".eposta-err");
const mesajError = document.querySelector(".mesaj-err");

iletisimForm?.addEventListener("submit", function (e) {
    e.preventDefault();
    let isValid = true;

    if (adSoyadInput.value.trim() === "") {
        adSoyadError.classList.remove("d-none");
        isValid = false;
    } else {
        adSoyadError.classList.add("d-none");
    }

    if (epostaInput.value.trim() === "") {
        epostaError.classList.remove("d-none");
        isValid = false;
    } else {
        epostaError.classList.add("d-none");
    }

    if (mesajTextarea.value.trim() === "") {
        mesajError.classList.remove("d-none");
        isValid = false;
    } else {
        mesajError.classList.add("d-none");
    }

    if (isValid) {
        const basariModal = new bootstrap.Modal(document.getElementById("exampleModal9"));
        basariModal.show();

        setTimeout(() => {
            basariModal.hide();
            iletisimForm.submit();
        }, 3000);
        iletisimForm.reset();
    }
});


// ----- kitap düzeltme ------
//resim yükleme
const uploadContainer = document.getElementById("upload-container");
const fileInput = document.getElementById("file-input");

uploadContainer?.addEventListener("click", () => {
    fileInput.click();
});

fileInput?.addEventListener("change", (event) => {
    const file = event.target.files[0];
    if (file) {
        const reader = new FileReader();
        reader.onload = (e) => {
            uploadContainer.innerHTML = '<img src="${e.target.result}" alt="Uploaded Image" style="max-width: 100%;">';
        };
        reader.readAsDataURL(file);
    }
});

//butona tıklama olayı
const btnKaydet = document.querySelector(".btn-kaydet");
btnKaydet?.addEventListener("click", function () {
    const bootstrapKitapModal = bootstrap.Modal.getInstance(
        document.querySelector("#exampleModal5")
    );
    bootstrapKitapModal.hide();

    const bootstrapBasariModal = new bootstrap.Modal(
        document.querySelector("#exampleModal55")
    );
    bootstrapBasariModal.show();

    setTimeout(() => bootstrapBasariModal.hide(), 3000);
});

// ----- kategori silme başarılı ------
const btnSil = document.getElementById("btnSil");
const silmeModalElement = document.getElementById("exampleModal4");
const silmeBasariModalElement = document.getElementById("exampleModal44");

btnSil.addEventListener("click", function () {
    const bootstrapSilmeModal =
        bootstrap.Modal.getInstance(silmeModalElement);
    bootstrapSilmeModal.hide();

    silmeModalElement.addEventListener(
        "hidden.bs.modal",
        function () {
            const bootstrapSilmeBasariModal = new bootstrap.Modal(
                silmeBasariModalElement
            );
            bootstrapSilmeBasariModal.show();

            setTimeout(() => {
                bootstrapSilmeBasariModal.hide();
            }, 3000);
        },
        {
            once: true,
        }
    ); // Bu olayın sadece bir kez çalışmasını sağlar
});

// ------- şifre değiştirme validation ------
const sifreform = document.querySelector(".sifre-degistir-form");
const ogrNoInput = document.getElementById("ogrenciNo-giris");
const sifreInput = document.getElementById("sifre");
const sifreTekrarInput = document.getElementById("sifre-tekrar");

const ogrNoError = document.querySelector(".ogrenciNo-input");
const sifreError = document.querySelector(".ogrenci-sifre-error");
const sifreTekrarError = document.querySelector(".sifre-tekrar-error");

const sifreBasariModal = document.getElementById("exampleModal8");

sifreform?.addEventListener("submit", (e) => {
    e.preventDefault();

    let isValid = true;

    const ogrenciNoValue = ogrNoInput.value.trim();
    if (ogrenciNoValue.length === 11 && /^[0-9]+$/.test(ogrenciNoValue)) {
        ogrNoError.classList.add("d-none");
    } else {
        ogrNoError.classList.remove("d-none");
        isValid = false;
    }

    const sifreValue = sifreInput.value.trim();
    if (sifreValue.length >= 4 && sifreValue.length <= 8) {
        sifreError.classList.add("d-none");
    } else {
        sifreError.classList.remove("d-none");
        isValid = false;
    }

    const sifreTekrarValue = sifreTekrarInput.value.trim();
    if (sifreValue === sifreTekrarValue) {
        sifreTekrarError.classList.add("d-none");
    } else {
        sifreTekrarError.classList.remove("d-none");
        isValid = false;
    }

    if (isValid) {
        const bootstrapModal = new bootstrap.Modal(sifreBasariModal);
        bootstrapModal.show();

        setTimeout(() => {
            bootstrapModal.hide();
            sifreform.submit();
        }, 3000);
    }
});


// -------- şifre açma ve gizleme --------
const eyes3 = document.querySelectorAll(".fa-eye");

eyes3.forEach((eyeIcon) => {
    eyeIcon.addEventListener("click", function () {
        const inputField =
            this.closest(".position-relative").querySelector("input");
        if (inputField.type === "password") {
            inputField.type = "text";
        } else {
            inputField.type = "password";
        }
    });
});

// -------- admin giriş validation --------
const adminForm = document.querySelector(".admin-form");
const tcInput = document.querySelector("#tcNo-giris");
const passwordInput = document.querySelector("#password-giris");
const tcError = document.querySelector(".tc-error");
const passwordError = document.querySelector(".sifre-error");

adminForm?.addEventListener("submit", (e) => {
    e.preventDefault();

    let isValid = true;

    const tcValue = tcInput.value.trim();
    if (tcValue.length === 11 && /^[0-9]+$/.test(tcValue)) {
        tcError.classList.add("d-none");
    } else {
        tcError.classList.remove("d-none");
        isValid = false;
    }

    const passwordValue = passwordInput.value.trim();
    if (passwordValue.length >= 4 && passwordValue.length <= 12) {
        passwordError.classList.add("d-none");
    } else {
        passwordError.classList.remove("d-none");
        isValid = false;
    }

    if (isValid) {
        window.open("anasayfa-admin.php", "_blank");
    }
});

// -------- şifre açma ve gizleme --------
const eyes2 = document.querySelectorAll(".fa-eye");

eyes2.forEach((eyeIcon) => {
    eyeIcon.addEventListener("click", function () {
        const inputField =
            this.closest(".position-relative").querySelector("input");
        if (inputField.type === "password") {
            inputField.type = "text";
        } else {
            inputField.type = "password";
        }
    });
});

const ogrenciLink = document.querySelector(".ogrenci-link");
const adminLink = document.querySelector(".admin-link");

ogrenciLink.addEventListener("click", function () {
    window.open("ogrenci-giris.php", "_blank");
});

adminLink.addEventListener("click", function () {
    window.open("admin-giris.php", "_blank");
});

// --------- öğrenci giriş validation ---------
const ogrGirisForm = document.querySelector(".ogr-giris-form");
const ogrenciNoInput = document.getElementById("ogrenciNo-giris");
const sifreİnp = document.getElementById("password-giris");

const ogrenciNoError = document.querySelector(".ogrNo-err");
const sifreErr = document.querySelector(".sifre-err");

ogrGirisForm?.addEventListener("submit", (e) => {
    e.preventDefault();

    let isValid = true;

    const ogrenciNoValue = ogrenciNoInput.value.trim();
    if (ogrenciNoValue.length === 11 && /^[0-9]+$/.test(ogrenciNoValue)) {
        ogrenciNoError.classList.add("d-none");
    } else {
        ogrenciNoError.classList.remove("d-none");
        isValid = false;
    }

    const passwordValue = sifreİnp.value.trim();
    if (passwordValue.length >= 4) {
        sifreErr.classList.add("d-none");
    } else {
        sifreErr.classList.remove("d-none");
        isValid = false;
    }

    if (isValid) {
        ogrGirisForm.submit();
    }
});

// -------- uye olma validation --------
const ogrUyeForm = document.querySelector(".uyeOl-form");
const ogrenciNoInp = document.getElementById("ogrenciNo-uye");
const adSoyadInp = document.getElementById("ad-uye");
const passwordInp = document.getElementById("password-uye");
const kvkkCheckbox = document.getElementById("exampleCheck1");
const sifreTekrarInp = document.getElementById("password-giris-tekrar");

const ogrenciNoErr = document.querySelector(".girisNo-err");
const adSoyadErr = document.querySelector(".ad-error");
const passwordErr = document.querySelector(".sifreErr");
const kvkkErr = document.querySelector(".kvkk-error");
const sifreTekrarErr = document.querySelector(".sifre-tekrar-err");

ogrUyeForm?.addEventListener("submit", (e) => {
    e.preventDefault();

    let isValid = true;

    const ogrenciNoValue = ogrenciNoInp.value.trim();
    if (ogrenciNoValue.length === 11 && /^[0-9]+$/.test(ogrenciNoValue)) {
        ogrenciNoErr.classList.add("d-none");
    } else {
        ogrenciNoErr.classList.remove("d-none");
        isValid = false;
    }

    const adSoyadValue = adSoyadInp.value.trim();
    if (adSoyadValue.length > 0) {
        adSoyadErr.classList.add("d-none");
    } else {
        adSoyadErr.classList.remove("d-none");
        isValid = false;
    }

    const passwordValue = passwordInp.value.trim();
    if (passwordValue.length >= 4 && passwordValue.length <= 8) {
        passwordErr.classList.add("d-none");
    } else {
        passwordErr.classList.remove("d-none");
        isValid = false;
    }

    const sifreTekrarVal = sifreTekrarInp.value.trim();
    if (passwordValue === sifreTekrarVal) {
        sifreTekrarErr.classList.add("d-none");
    } else {
        sifreTekrarErr.classList.remove("d-none");
        isValid = false;
    }

    if (kvkkCheckbox.checked) {
        kvkkErr.classList.add("d-none");
    } else {
        kvkkErr.classList.remove("d-none");
        isValid = false;
    }

    if (isValid) {
        ogrUyeForm.submit();
    }
});

// ------ sidebar'daki kategori ve kitap listlerini açma ve kapama ------
const btnKategori = document.querySelector(".btn-kategori");
const kategoriList = document.querySelector(".kategori-list");

btnKategori.addEventListener("click", function () {
    kategoriList.classList.toggle("d-none");
});

const btnKitap = document.querySelector(".btn-kitap");
const kitapList = document.querySelector(".kitap-list");

btnKitap?.addEventListener("click", function () {
    kitapList.classList.toggle("d-none");
});

// ----- odeme başarıyla gerçekleşme -----
const odemeForm = document.querySelector(".odemeForm");
const odemeModal = document.querySelector("#exampleModal7");
const basariOdemeModal = document.querySelector("#exampleModal77");
const KartErr = document.querySelector(".kart-error");
const kartInpt = document.querySelector(".kartNo");

odemeForm?.addEventListener("submit", function (e) {
    e.preventDefault();

    if (kartInpt.value.trim() === "" || kartInpt.value.length < 16 || isNaN(kartInpt.value)) {
        KartErr.classList.replace("d-none", "d-block");
    }
    else {
        const bootstrapKategoriModal = bootstrap.Modal.getInstance(odemeModal);
        bootstrapKategoriModal.hide();

        const bootstrapBasariModal = new bootstrap.Modal(basariOdemeModal);
        bootstrapBasariModal.show();

        setTimeout(() => {
            bootstrapBasariModal.hide();
        }, 3000);

        odemeForm.reset();

        KartErr.classList.replace("d-block", "d-none");
    }
});

odemeModal?.addEventListener('hidden.bs.modal', function () {
    odemeForm.reset();
    KartErr.classList.add("d-none");
});