def encrypt(text, key, alph):
    encrypted_text = ""
    i = 0
    for char in text:
        if char in alph:
            char_index = alph.index(char)
            new_index = (char_index + int(key[i%len(key)])) % len(alph)
            encrypted_text += alph[new_index]
            i+=1
        else:
            encrypted_text += char
    return encrypted_text

def decrypt(encrypted_text, key, alph):
    decrypted_text = ""
    i = 0
    for char in encrypted_text:
        if char in alph:
            char_index = alph.index(char)
            new_index = (char_index - int(key[i%len(key)])) % len(alph)
            decrypted_text += alph[new_index]
            i +=1
        else:
            decrypted_text += char
    return decrypted_text

text = "ассоциативнодинамическаятрансляция".upper()
key = '76683'
alph = 'АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ'

encrypted_text = encrypt(text, key, alph)
print("Зашифрованный текст:", encrypted_text)

decrypted_text = decrypt(encrypted_text, key, alph)
print("Расшифрованный текст:", decrypted_text)
