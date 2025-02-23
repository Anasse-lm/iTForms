
// Initialize SortableJS for drag-and-drop functionality
// export function initializeSortable(containerElement, dotNetHelper) {
//     new Sortable(containerElement, {
//         animation: 150,
//         handle: '.drag-handle',
//         onEnd: function(evt) {
//             dotNetHelper.invokeMethodAsync('UpdateQuestionOrder', evt.oldIndex, evt.newIndex);
//         }
//     });
// }

// Handle image upload to cloud storage
export async function handleImageUpload(fileInput) {
    try {
        const formData = new FormData();
        const file = fileInput.files[0];
        
        // Basic validation
        if (!file) throw new Error('No file selected');
        if (!file.type.startsWith('image/')) throw new Error('Invalid file type');

        formData.append('file', file);
        
        const response = await fetch('/api/upload', {
            method: 'POST',
            body: formData
        });

        if (!response.ok) throw new Error('Upload failed');
        
        return await response.json(); // Should return { url: "https://..." }
        
    } catch (error) {
        console.error('Upload error:', error);
        throw error; // Rethrow for Blazor to handle
    }
}

// Toast notification system
export function showToast(message, type = 'info') {
    const toast = document.createElement('div');
    toast.className = `toast toast-${type}`;
    toast.innerHTML = message;
    document.body.appendChild(toast);
    
    setTimeout(() => toast.remove(), 3000);
}