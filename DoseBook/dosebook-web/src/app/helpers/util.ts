function contains(text: string, textToSearch: string): boolean {
    const val1Trimmed = text.trim().toLowerCase();
    const val2rimmed = textToSearch.trim().toLowerCase();
    return !!~val1Trimmed.indexOf(val2rimmed);
}


function toAge(dob: Date): number {
    const timeDiff = Math.abs(Date.now() - dob.getTime());
    return Math.floor((timeDiff / (1000 * 3600 * 24)) / 365);
}

function toAgeClass(dob: Date): string {
    const timeDiff = Math.abs(Date.now() - dob.getTime());
    const age = Math.floor((timeDiff / (1000 * 3600 * 24)) / 365);
    return 'age' + '_' + getRange(age);

}


function getRange(num: number, lower: number = 0): string {
    const upperRange = lower + 10;
    if (num >= lower && num <= upperRange) {
        return lower + '_' + upperRange;
    } else {
        getRange(num, upperRange);
    }
}

export { contains, toAge, toAgeClass };
