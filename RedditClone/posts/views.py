from django.shortcuts import render, redirect
from django.contrib.auth.decorators import login_required
from django.utils import timezone
from .models import Post
from django.contrib.auth.models import User



@login_required
def create(request):
    if request.method == 'POST':
        if request.POST['title'] and request.POST['url']:
            post = Post()
            post.title = request.POST['title']
            if request.POST['url'].startswith("http://") or request.POST['url'].startswith("https://"):
                post.url = request.POST['url']
                # add catch for missing.com
            else:
                post.url = 'http://' + request.POST['url']
            post.datePublished = timezone.datetime.now()
            post.author = request.user
            post.save()
            return redirect('home')
        else:
            return render(request, 'posts/create.html', {"error": 'ERROR: You must include a title and a URL to post'})
    else:
        return render(request, 'posts/create.html')


def home(request):
    posts = Post.objects.order_by('-votesTotal')
    return render(request, 'posts/home.html', {'posts': posts})


def upvote(request, pk):
    if request.method == 'POST':
        post = Post.objects.get(pk=pk)
        post.votesTotal += 1
        post.save()
        return redirect('home')


def downvote(request, pk):
    if request.method == 'POST':
        post = Post.objects.get(pk=pk)
        post.votesTotal -= 1
        post.save()
        return redirect('home')


def userPosts(request, fk):
    posts = Post.objects.filter(author__id=fk).order_by('-votesTotal')
    author = User.objects.get(pk=fk)
    return render(request, 'posts/userPosts.html', {'posts': posts, 'author': author})
