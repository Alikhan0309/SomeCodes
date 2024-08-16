from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from selenium import webdriver
from selenium.webdriver.chrome.service import Service as ChromeService
from webdriver_manager.chrome import ChromeDriverManager
from bs4 import BeautifulSoup
import re

# Настройка Chrome-драйвера
driver = webdriver.Chrome(service=ChromeService(ChromeDriverManager().install()))
url = 'https://studyenglishwords.com/book/1984-%D0%A1%D0%BA%D0%BE%D1%82%D0%BD%D1%8B%D0%B9-%D0%94%D0%B2%D0%BE%D1%80/237?page=1'

pages = list()

try:
    while True:  # Цикл, пока есть страницы
        driver.get(url)
        # Ожидаем, пока элементы на странице полностью загрузятся
        WebDriverWait(driver, 10).until(EC.presence_of_element_located((By.CSS_SELECTOR, 'div.left')))
        html = driver.page_source
        soup = BeautifulSoup(html, 'html.parser')

        # Находим все элементы с текстом на английском и русском
        english = soup.find_all('div', {'class': 'left'})
        russian = soup.find_all('div', {'class': 'right'})

        en_text = ""
        ru_text = ""
        dict_ = dict()

        for i in range(len(english)):
            en = english[i].text
            ru = russian[i].text
            # Убираем лишние пробелы и пустые строки
            text_en = re.sub(' +', ' ', en)
            text_en = re.sub('\n+', '\n', text_en)
            text_ru = re.sub(' +', ' ', ru)
            text_ru = re.sub('\n+', '\n', text_ru)
            en_text += text_en
            ru_text += text_ru

        dict_["en"] = en_text
        dict_["ru"] = ru_text
        pages.append(dict_)

        try:
            # Пытаемся найти ссылку на следующую страницу
            next_page_link = driver.find_element(By.CSS_SELECTOR, 'span.next')
            url = next_page_link.find_element(By.XPATH, './a').get_attribute('href')
        except:
            # Если ссылки нет, выходим из цикла
            break

finally:
    # Закрываем браузер
    driver.quit()

# Сохраняем собранные данные в файлы
with open('C:/Users/user/Desktop/english.txt', 'a', encoding='utf-8') as file:
    for page in pages:
        file.write(page["en"])

with open('C:/Users/user/Desktop/russian.txt', 'a', encoding='utf-8') as file:
    for page in pages:
        file.write(page["ru"])
