import type {IAccount} from "../types/account/IAccount.ts";

export function checkLogin(): boolean {
    const token = localStorage.getItem("token");
    return !!token;
}

export function logout(): void {
    localStorage.removeItem("token");
}

export function getUserInfo(): IAccount | null {
    const token = localStorage.getItem("token");
    if (!token) return null;

    try {
        const payloadBase64 = token.split(".")[1];
        const payloadDecoded = decodeURIComponent(
            atob(payloadBase64)
                .split("")
                .map((c) => "%" + ("00" + c.charCodeAt(0).toString(16)).slice(-2))
                .join("")
        );
        const payload = JSON.parse(payloadDecoded);

        const { email, name, image, roles } = payload;
        return { email, name, image, roles };
    } catch (e) {
        console.error("Помилка розшифрування токена:", e);
        return null;
    }
}