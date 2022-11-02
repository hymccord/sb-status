export async function isSbUp(): Promise<boolean> {
    return await fetch('https://sponsor.ajay.app/api/status')
        .then(response => {
            return response.status === 200;
        })
        .catch(() => {
            return false;
        })
}